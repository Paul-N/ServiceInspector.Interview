using Microsoft.Maui.Platform;

namespace ServiceInspector.Interview02.Maui.Controls
{
    public class BackgroundAwareButton : Button
    {
        public static readonly BindableProperty CustomBackgroundColorProperty =
            BindableProperty.Create(nameof(CustomBackgroundColor), typeof(Color), typeof(BackgroundAwareButton), propertyChanged: OnCustomBackgroundColorChanged);

        public Color CustomBackgroundColor
        {
            get => (Color)GetValue(CustomBackgroundColorProperty);
            set => SetValue(CustomBackgroundColorProperty, value);
        }

        static void OnCustomBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (BackgroundAwareButton)bindable;
            var backgroundColor = control.CustomBackgroundColor;

            if (control.Handler is null || control.Handler.PlatformView is null)
            {
                // Workaround for when this executes the Handler and PlatformView is null
                control.HandlerChanged += OnHandlerChanged;
                return;
            }

            if (backgroundColor is not null)
            {
#if ANDROID
            if (control.Handler.PlatformView is Android.Widget.Button nativeBtn)
                nativeBtn.SetBackgroundColor(backgroundColor.ToPlatform());
#elif IOS || MACCATALYST
            if (control.Handler.PlatformView is UIKit.UIButton nativeBtn)
                nativeBtn.BackgroundColor = backgroundColor.ToPlatform();
#endif
            }

            void OnHandlerChanged(object s, EventArgs e)
            {
                OnCustomBackgroundColorChanged(control, oldValue, newValue);
                control.HandlerChanged -= OnHandlerChanged;
            }
        }
    }
}
