using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace rush00.App.Views
{
    public class HabitCreateView : UserControl
    {
        public HabitCreateView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
