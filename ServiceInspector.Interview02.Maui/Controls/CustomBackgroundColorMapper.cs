using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace ServiceInspector.Interview02.Maui.Controls
{
    public static class CustomBackgroundColorMapper
    {
        private const string _propertyName = "CustomBackgroundColor";

        public static readonly BindableProperty CustomBackgroundColorProperty = BindableProperty.CreateAttached(_propertyName, typeof(Color), typeof(Button), null);

        public static Color GetCustomBackgroundColor(BindableObject view) => (Color)view.GetValue(CustomBackgroundColorProperty);

        public static void SetCustomBackgroundColor(BindableObject view, Color value) => view.SetValue(CustomBackgroundColorProperty, value);

        public static void ApplyCustomBackgroundColor()
        {
            ButtonHandler.Mapper.Add(_propertyName, (handler, view) =>
            {
                var backgroundColor = GetCustomBackgroundColor((Button)handler?.VirtualView);

                if (backgroundColor == null)
                    return;

                SetCustomBackgroundColor(handler, backgroundColor);

            });
        }

#if ANDROID
        private static void SetCustomBackgroundColor(IViewHandler handler, Color backgroundColor)
        {
            if(handler?.PlatformView is Google.Android.Material.Button.MaterialButton nativeBtn)
                nativeBtn?.SetBackgroundColor(backgroundColor.ToPlatform());
        }

#elif IOS || MACCATALYST
        private static void SetCustomBackgroundColor(IViewHandler handler, Color backgroundColor)
        {
            if (handler?.PlatformView is UIKit.UIButton nativeBtn)
                nativeBtn.BackgroundColor = backgroundColor.ToPlatform();
        }
#endif
    }
}
