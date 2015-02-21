using System.Windows.Input;

namespace ScottIsAFool.Windows.Core.Interfaces
{
    public interface ICanHasHomeButton
    {
        bool ShowHomeButton { get; set; }
        ICommand NavigateHomeCommand { get; }
    }
}