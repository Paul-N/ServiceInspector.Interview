using System.ComponentModel;

namespace ServiceInspector.Interview02.Maui.Controls
{
    public class CustomBackgroundColorBehavior : Behavior<Button>
    {
        private const string _propertyName = "CustomBackgroundColor";

        public static readonly BindableProperty CustomBackgroundColorProperty
            = BindableProperty.Create(_propertyName, typeof(Color), typeof(CustomBackgroundColorBehavior), propertyChanged: OnCustomBackgroundColorChanged);


        public static Color? GetCustomBackgroundColor(BindableObject view) => (Color?)view.GetValue(CustomBackgroundColorProperty);

        public static void SetCustomBackgroundColor(BindableObject view, Color? value) => view.SetValue(CustomBackgroundColorProperty, value);

        private Button? Button { get; set; }

        private static void OnCustomBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is CustomBackgroundColorBehavior behavior && behavior.Button is Button button)
                NativeBackgroundColorExtensions.HandleCustomBackgroundColorChanged(button, newValue as Color);
        }

        protected override void OnAttachedTo(Button bindable)
        {
            base.OnAttachedTo(bindable);
            Button = bindable;

            var color = GetCustomBackgroundColor(this);
            NativeBackgroundColorExtensions.HandleCustomBackgroundColorChanged(bindable, color);
        }

        protected override void OnDetachingFrom(Button bindable)
        {
            Button = null;
            base.OnDetachingFrom(bindable);
        }
    }
}
