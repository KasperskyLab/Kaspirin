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

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Notifications;

[StyleTypedProperty(Property = "ContentPanelStyle", StyleTargetType = typeof(Control))]
[DefaultProperty("Content")]
[ContentProperty("Content")]
public sealed class NotificationAction : TriggerAction<FrameworkElement>
{
    public NotificationAction()
    {
        _tracer = ComponentTracer.Get(UIKitComponentTracers.Notification, this);
    }

    #region Content
    [Bindable(true)]
    public object Content
    {
        get => GetValue(ContentControl.ContentProperty);
        set => SetValue(ContentControl.ContentProperty, value);
    }

    #endregion

    #region ContentTemplate

    public DataTemplate ContentTemplate
    {
        get => (DataTemplate)GetValue(ContentTemplateProperty);
        set => SetValue(ContentTemplateProperty, value);
    }

    public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register(
        nameof(ContentTemplate),
        typeof(DataTemplate),
        typeof(NotificationAction),
        new PropertyMetadata(default(DataTemplate)));

    #endregion

    #region DisplayMode

    public NotificationDisplayMode DisplayMode
    {
        get => (NotificationDisplayMode)GetValue(DisplayModeProperty);
        set => SetValue(DisplayModeProperty, value);
    }

    public static readonly DependencyProperty DisplayModeProperty = DependencyProperty.Register(
        nameof(DisplayMode),
        typeof(NotificationDisplayMode),
        typeof(NotificationAction),
        new PropertyMetadata(NotificationDisplayMode.Modal));

    #endregion

    #region LayerName

    public string LayerName
    {
        get => (string)GetValue(LayerNameProperty);
        set => SetValue(LayerNameProperty, value);
    }

    public static readonly DependencyProperty LayerNameProperty = DependencyProperty.Register(
        nameof(LayerName),
        typeof(string),
        typeof(NotificationAction),
        new PropertyMetadata(NotificationLayer.DefaultLayerName));

    #endregion

    protected override void Invoke(object parameter)
    {
        _tracer.TraceMethodCall();

        var interactionObject = Guard.EnsureArgumentIsInstanceOfType<InteractionRequestedEventArgs>(parameter).InteractionObject;

        Guard.Assert(interactionObject.IsDecided == false);
        Guard.IsNotNull(AssociatedObject);
        Guard.AssertIsUiThread();

        var displaySettings = new NotificationDisplaySettings()
        {
            LayerName = LayerName,
            DisplayMode = DisplayMode,
        };

        var view = NotificationLauncher.Create(
            associatedObject: AssociatedObject,
            content: Content ?? interactionObject.GetDataContext(),
            contentTemplate: ContentTemplate,
            displaySettings: displaySettings);

        interactionObject.Decided += () => OnDecided(view);

        view.Opening += (s, e) => OnOpening(interactionObject);
        view.Closing += (s, e) => OnClosing(interactionObject);

        view.SetInteractionObject(interactionObject);
        view.Show();
    }

    private void OnOpening(InteractionObject interactionObject)
    {
        interactionObject.InteractionStarted();
    }

    private void OnClosing(InteractionObject interactionObject)
    {
        if (interactionObject.IsDecided is false)
        {
            interactionObject.Handle();
        }

        interactionObject.InteractionCompleted();
    }

    private void OnDecided(NotificationView notificationView)
    {
        if (notificationView.State.NotIn(NotificationViewState.Closing, NotificationViewState.Closed))
        {
            notificationView.Close();
        }
    }

    private readonly ComponentTracer _tracer;
}
