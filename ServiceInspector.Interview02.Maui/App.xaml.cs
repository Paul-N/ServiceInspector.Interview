using ServiceInspector.Interview02.Maui.Controls;

namespace ServiceInspector.Interview02.Maui
{
    public partial class App : Application
    {
        public App()
        {
            CustomBackgroundColorMapper.ApplyCustomBackgroundColor();
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}