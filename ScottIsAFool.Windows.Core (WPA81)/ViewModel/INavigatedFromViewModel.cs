using Windows.UI.Xaml.Navigation;

namespace ScottIsAFool.Windows.Core.ViewModel
{
    public interface INavigatedFromViewModel
    {
        void NavigatedFrom(NavigationEventArgs e);
    }
}