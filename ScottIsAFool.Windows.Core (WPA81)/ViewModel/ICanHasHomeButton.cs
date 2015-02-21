using System.Windows.Input;

namespace ScottIsAFool.Windows.Core.ViewModel
{
    public interface ICanHasHomeButton
    {
        bool ShowHomeButton { get; set; }
        ICommand NavigateHomeCommand { get; }
    }
}