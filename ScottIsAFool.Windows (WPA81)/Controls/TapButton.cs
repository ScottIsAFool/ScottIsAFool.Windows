using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace ScottIsAFool.Windows.Controls
{
    public class TapButton : Button
    {
        public static readonly DependencyProperty TapCommandProperty = DependencyProperty.Register(
            "TapCommand", typeof (ICommand), typeof (TapButton), new PropertyMetadata(default(ICommand)));

        public ICommand TapCommand
        {
            get { return (ICommand) GetValue(TapCommandProperty); }
            set { SetValue(TapCommandProperty, value); }
        }

        public TapButton()
        {
            Tapped += OnTap;
        }

        private void OnTap(object sender, TappedRoutedEventArgs e)
        {
            if (TapCommand != null)
            {
                if (TapCommand.CanExecute(CommandParameter))
                {
                    TapCommand.Execute(CommandParameter);
                }
            }
        }
    }
}