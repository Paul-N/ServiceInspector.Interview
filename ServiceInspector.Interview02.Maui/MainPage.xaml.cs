namespace ServiceInspector.Interview02.Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
            {
                var text = $"Clicked {count} time";
                CounterBtn.Text = text;
                CounterBtn2.Text = text;
                CounterBtn3.Text = text;
            }
            else
            {
                var text = $"Clicked {count} times";
                CounterBtn.Text = text;
                CounterBtn2.Text = text;
                CounterBtn3.Text = text;
            }

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}