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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using Kaspirin.UI.Framework.UiKit.ThirdParty.HtmlUtils;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class HtmlBlock : Section
    {
        public HtmlBlock()
        {
            AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(OnHyperlinkRequestNavigateEvent));
        }

        #region Html

        public string? Html
        {
            get => (string?)GetValue(HtmlProperty);
            set => SetValue(HtmlProperty, value);
        }

        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.Register(nameof(Html), typeof(string), typeof(HtmlBlock),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnHtmlChanged));

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var htmlBlock = (HtmlBlock)d;
            htmlBlock.UpdateContent();
        }

        #endregion

        #region HtmlSource

        public Uri? HtmlSource
        {
            get => (Uri?)GetValue(HtmlSourceProperty);
            set => SetValue(HtmlSourceProperty, value);
        }

        public static readonly DependencyProperty HtmlSourceProperty =
            DependencyProperty.Register(nameof(HtmlSource), typeof(Uri), typeof(HtmlBlock),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnHtmlSourceChanged));

        private static void OnHtmlSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var htmlBlock = (HtmlBlock)d;
            htmlBlock.LoadHtmlFromSource();
        }

        #endregion

        #region BasePath

        public string? BasePath
        {
            get => (string)GetValue(BasePathProperty);
            set => SetValue(BasePathProperty, value);
        }

        public static readonly DependencyProperty BasePathProperty =
            DependencyProperty.Register(nameof(BasePath), typeof(string), typeof(HtmlBlock),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnBasePathChanged));

        private static void OnBasePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.ReadLocalValue(HtmlProperty) != DependencyProperty.UnsetValue)
            {
                var htmlBlock = (HtmlBlock)d;
                htmlBlock.UpdateContent();
            }
        }

        #endregion

        #region HasParsingErrors

        public bool HasParsingErrors
        {
            get => (bool)GetValue(HasParsingErrorsProperty);
            private set => SetValue(_hasParsingErrorsPropertyKey, value);
        }

        private static readonly DependencyPropertyKey _hasParsingErrorsPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(HasParsingErrors), typeof(bool), typeof(HtmlBlock),
                new PropertyMetadata(false));

        public static readonly DependencyProperty HasParsingErrorsProperty =
            _hasParsingErrorsPropertyKey.DependencyProperty;

        #endregion

        #region HyperlinkNavigation

        public IHtmlBlockNavigationCallback? HyperlinkNavigation
        {
            get => (IHtmlBlockNavigationCallback?)GetValue(HyperlinkNavigationProperty);
            set => SetValue(HyperlinkNavigationProperty, value);
        }

        public static readonly DependencyProperty HyperlinkNavigationProperty =
            DependencyProperty.Register(
                nameof(HyperlinkNavigation),
                typeof(IHtmlBlockNavigationCallback),
                typeof(HtmlBlock),
                new FrameworkPropertyMetadata(new DefaultHyperlinkNavigation()));

        #endregion

        private void LoadHtmlFromSource()
        {
            var filePath = HtmlSource?.LocalPath;

            if (filePath != null && File.Exists(filePath))
            {
                Html = File.ReadAllText(filePath);
            }
            else
            {
                Html = string.Empty;
                _tracer.TraceError($"Can`t load content from file [{filePath}]");
                HasParsingErrors = true;
            }
        }

        private void UpdateContent()
        {
            ClearContent();

            var htmlString = Html;

            if (string.IsNullOrEmpty(htmlString))
            {
                return;
            }

            htmlString = TrimBomPreamble(htmlString);

            var basePath = BasePath
                ?? Path.GetDirectoryName(HtmlSource?.LocalPath)
                ?? Directory.GetCurrentDirectory();

            var xaml = HtmlToXamlConverter.ConvertHtmlToXaml(htmlString, asFlowDocument: false, basePath);
            if (!string.IsNullOrEmpty(xaml))
            {
                HasParsingErrors = !TryParseXaml(xaml, out var contentBlock);
                if (!HasParsingErrors)
                {
                    Blocks.Add(contentBlock);
                }
            }
        }

        private bool TryParseXaml(string xaml, out Block? contentBlock)
        {
            try
            {
                contentBlock = (Block)System.Windows.Markup.XamlReader.Parse(xaml);
                return true;
            }
            catch (Exception ex)
            {
                _tracer.TraceError(ex.Message);
                contentBlock = null;
                return false;
            }
        }

        private string TrimBomPreamble(string html)
        {
            var bom = Encoding.Unicode.GetChars(Encoding.Unicode.GetPreamble());

            return html.TrimStart(bom);
        }

        private void ClearContent()
        {
            Blocks.Clear();
            HasParsingErrors = false;
        }

        private void OnHyperlinkRequestNavigateEvent(object sender, RequestNavigateEventArgs e)
        {
            HyperlinkNavigation?.Navigate(e.Uri, e.Target);
            e.Handled = true;
        }

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(nameof(HtmlBlock));

        private sealed class DefaultHyperlinkNavigation : IHtmlBlockNavigationCallback
        {
            public void Navigate(Uri? uri, string? target)
            {
                if (uri?.IsAbsoluteUri == true)
                {
                    var url = uri?.AbsoluteUri;
                    if (!string.IsNullOrEmpty(url))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = url,
                            UseShellExecute = true
                        });
                    }
                }
            }
        }
    }
}
