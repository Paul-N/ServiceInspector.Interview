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
            => NativeBackgroundColorExtensions.HandleCustomBackgroundColorChanged(bindable, (bindable as BackgroundAwareButton)?.BackgroundColor);
    }
}
