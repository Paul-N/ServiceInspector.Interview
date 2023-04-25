using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var nativeBtn = control.Handler.PlatformView as Android.Widget.Button;
                if (nativeBtn != null)
                {
                    nativeBtn.SetBackgroundColor(backgroundColor.ToPlatform());
                }
#elif IOS || MACCATALYST
                var nativeBtn = control.Handler.PlatformView as UIKit.UIButton;
                if (nativeBtn != null)
                {
                    nativeBtn.BackgroundColor = backgroundColor.ToPlatform();
                }
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
