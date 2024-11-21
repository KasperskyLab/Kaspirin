// Copyright © 2024 AO Kaspersky Lab.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public sealed class WindowSettingsPersistentStorage : PersistentStorageBase
    {
        public WindowSettingsPersistentStorage(IKeyValueStorage keyValueStorage, Window window)
            : base(keyValueStorage)
        {
            Guard.ArgumentIsNotNull(window);

            _window = window;
            _windowId = WindowIdFactory.GetDefaultWindowId(window);
        }

        public const string HeightPropertyName = "height";
        public const string WidthPropertyName = "width";
        public const string PositionXPropertyName = "positionX";
        public const string PositionYPropertyName = "positionY";
        public const string IsMaximizedPropertyName = "isMaximized";
        public const string DpiXName = "dpiX";
        public const string DpiYName = "dpiY";
        public const string SettingRoot = "WindowsSettings";

        public bool SettingsExists()
        {
            return TryGetWindowDpi(_windowId, out _) &&
                   TryGetParameter(_windowId, HeightPropertyName, out int _) &&
                   TryGetParameter(_windowId, WidthPropertyName, out int _) &&
                   TryGetPosition(_windowId, out _);
        }

        public WindowSettings GetSettings(WindowSettings? defaults = null)
        {
            var storedSettings = new WindowSettings() { Id = _windowId };
            var defaultSettings = defaults ?? WindowScreenMonitoringService.GetInstance(_window).GetWindowSettings();

            WindowSettings settings;

            if (TryGetWindowDpi(_windowId, out var windowDpi))
            {
                settings = storedSettings;
                settings.WindowDpi = windowDpi;

                if (TryGetParameter(_windowId, HeightPropertyName, out int height) &&
                    TryGetParameter(_windowId, WidthPropertyName, out int width))
                {
                    settings.Width = width;
                    settings.Height = height;
                }
                else
                {
                    settings.Width = defaultSettings.Width;
                    settings.Height = defaultSettings.Height;
                }

                settings.Position = TryGetPosition(_windowId, out var position)
                    ? position
                    : defaultSettings.Position;

                settings.IsMaximized = TryGetParameter(_windowId, IsMaximizedPropertyName, out bool isMaximized)
                    ? isMaximized
                    : defaultSettings.IsMaximized;
            }
            else
            {
                settings = defaultSettings;
            }

            _trace.TraceInformation($"Settings provided for {settings.Id}: {settings}");
            return storedSettings;
        }

        public void SaveSettings(WindowSettings settings)
        {
            Guard.ArgumentIsNotNull(settings);
            Guard.Argument(settings.Id == _windowId, $"settings.Id \"{settings.Id}\" must be equals \"{_windowId}\"");

            if (!settings.IsMaximized)
            {
                TrySetColumnParameter(settings.Id, HeightPropertyName, settings.Height);
                TrySetColumnParameter(settings.Id, WidthPropertyName, settings.Width);
            }

            TrySetColumnParameter(settings.Id, IsMaximizedPropertyName, settings.IsMaximized);

            if (settings.Position.HasValue)
            {
                TrySetColumnParameter(settings.Id, PositionXPropertyName, (int)settings.Position.Value.X);
                TrySetColumnParameter(settings.Id, PositionYPropertyName, (int)settings.Position.Value.Y);
            }

            if (settings.WindowDpi != null)
            {
                TrySetColumnParameter(settings.Id, DpiXName, settings.WindowDpi.X);
                TrySetColumnParameter(settings.Id, DpiYName, settings.WindowDpi.Y);
            }

            _trace.TraceInformation($"Settings saved for {settings.Id}: {settings}");
        }

        private bool TryGetPosition(string windowId, out Point point)
        {
            if (TryGetParameter(windowId, PositionXPropertyName, out string? positionX) &&
                TryGetParameter(windowId, PositionYPropertyName, out string? positionY))
            {
                if (int.TryParse(positionX, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var x)
                    && int.TryParse(positionY, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var y))
                {
                    point = new Point(x, y);
                    return true;
                }
            }

            point = default;
            return false;
        }

        private bool TrySetPosition(string windowId, Point point)
        {
            return TrySetColumnParameter(windowId, PositionXPropertyName, (int)point.X) &&
                   TrySetColumnParameter(windowId, PositionYPropertyName, (int)point.Y);
        }

        private bool TryGetWindowDpi(string windowId, [NotNullWhen(true)] out Dpi? dpi)
        {
            if (TryGetParameter(windowId, DpiXName, out string? dpiX) &&
                TryGetParameter(windowId, DpiYName, out string? dpiY))
            {
                if (int.TryParse(dpiX, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var x) &&
                    int.TryParse(dpiY, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var y))
                {
                    dpi = new Dpi(x, y);
                    return true;
                }
            }

            dpi = default;
            return false;
        }

        private bool TryGetParameter<T>(string windowId, string propertyKey, out T? parameterValue)
        {
            try
            {
                var value = Read(GetPropertyKey(windowId, propertyKey));
                if (value != null)
                {
                    parameterValue = (T)Convert.ChangeType(value, typeof(T));
                    return true;
                }
            }
            catch (Exception)
            {
                _trace.TraceError($"Can not get value for {propertyKey}");
            }

            parameterValue = default(T);
            return false;
        }

        private bool TrySetColumnParameter<T>(string windowId, string propertyKey, T data)
        {
            if (typeof(T).IsClass && Equals(data, default(T)))
            {
                return false;
            }

            var value = Convert.ToString(data) ?? string.Empty;

            if (!Save(GetPropertyKey(windowId, propertyKey), value))
            {
                _trace.TraceInformation($"Failed to set column parameter: window {windowId},  param {propertyKey}, data {data}");
                return false;
            }

            return true;
        }

        private static string GetPropertyKey(string windowId, string propertyKey)
        {
            return string.Format("\\{0}\\{1}\\{2}", SettingRoot, windowId, propertyKey);
        }

        private static readonly ComponentTracer _trace = ComponentTracer.Get(nameof(WindowSettingsPersistentStorage));

        private readonly Window _window;
        private readonly string _windowId;
    }
}
