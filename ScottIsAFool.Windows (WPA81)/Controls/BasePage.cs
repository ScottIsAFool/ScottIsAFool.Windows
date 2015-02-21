using System.Linq;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Cimbalino.Toolkit.Services;
using ScottIsAFool.Windows.Core.Logging;
using ScottIsAFool.Windows.Core.ViewModel;
using ScottIsAFool.Windows.Helpers;

namespace ScottIsAFool.Windows.Controls
{
    public abstract class BasePage : Page
    {
        protected readonly ILog Logger;
        
        protected virtual ApplicationViewBoundsMode Mode
        {
            get { return ApplicationViewBoundsMode.UseVisible; }
        }

        public abstract INavigationService NavigationService { get; }

        protected BasePage()
        {
            NavigationCacheMode = NavigationCacheMode.Required;
            Logger = new WinLogger(GetType().FullName);
            //_navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
        }

        protected virtual void OnBackKeyPressed(object sender, NavigationServiceBackKeyPressedEventArgs e)
        {
        }

        protected void SetFullScreen(ApplicationViewBoundsMode mode)
        {
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(mode);
        }

        protected virtual void InitialiseOnBack()
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.BackKeyPressed += OnBackKeyPressed;
            }

            Logger.Info("Navigated to {0}", GetType().FullName);

            SetFullScreen(Mode);

            if (e.NavigationMode == NavigationMode.Back)
            {
                InitialiseOnBack();
            }

            var parameters = e.Parameter as NavigationParameters;
            if (parameters != null)
            {
                if (parameters.ClearBackstack)
                {
                    Logger.Info("Clearing backstack");
                    Frame.BackStack.Clear();
                }
            }

            var vm = DataContext as ICanHasHomeButton;
            if (vm != null)
            {
                vm.ShowHomeButton = parameters != null && parameters.ShowHomeButton;
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (NavigationService != null)
            {
                NavigationService.BackKeyPressed -= OnBackKeyPressed;
            }

            if (e.NavigationMode != NavigationMode.Back)
            {
                var parameters = e.Parameter as NavigationParameters;
                if (parameters != null && parameters.RemoveCurrentPageFromBackstack)
                {
                    var page = Frame.BackStack.FirstOrDefault(x => x.SourcePageType == GetType());
                    if (page != null)
                    {
                        Frame.BackStack.Remove(page);
                    }
                }
            }

            Logger.Info("Navigated from {0}", GetType().FullName);
            base.OnNavigatedFrom(e);
        }
    }
}
