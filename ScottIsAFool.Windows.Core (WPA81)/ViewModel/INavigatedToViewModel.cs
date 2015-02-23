using Windows.UI.Xaml.Navigation;

namespace ScottIsAFool.Windows.Core.ViewModel
{
    public interface INavigatedToViewModel
    {
        void NavigatedTo(NavigationEventArgs e);
    }
}