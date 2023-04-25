using Microsoft.Maui.Platform;

namespace ServiceInspector.Interview02.Maui.Controls
{
    public static class NativeBackgroundColorExtensions
    {
        public static void HandleCustomBackgroundColorChanged(BindableObject bindable, Color? backgroundColor)
        {
            var control = (Button)bindable;

            if (control.Handler is null || control.Handler.PlatformView is null)
            {
                // Workaround for when this executes the Handler and PlatformView is null
                control.HandlerChanged += OnHandlerChanged;
                return;
            }

            if (backgroundColor is not null)
            {
                control.Handler.SetBackgroundColor(backgroundColor);
            }

            void OnHandlerChanged(object? s, EventArgs e)
            {
                HandleCustomBackgroundColorChanged(control, backgroundColor);
                control.HandlerChanged -= OnHandlerChanged;
            }
        }

        public static void SetBackgroundColor(this IViewHandler handler, Color backgroundColor)
        {
#if ANDROID
            if (handler.PlatformView is Android.Widget.Button nativeBtn)
                nativeBtn.SetBackgroundColor(backgroundColor.ToPlatform());
#elif IOS || MACCATALYST
            if (handler.PlatformView is UIKit.UIButton nativeBtn)
                nativeBtn.BackgroundColor = backgroundColor.ToPlatform();
#endif
        }
    }
}
