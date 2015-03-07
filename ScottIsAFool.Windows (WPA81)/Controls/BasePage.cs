using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Cimbalino.Toolkit.Services;
using ScottIsAFool.Windows.Core.Logging;
using ScottIsAFool.Windows.Core.ViewModel;
using ScottIsAFool.Windows.Helpers;
#if WINDOWS_PHONE_APP
using Windows.UI.ViewManagement;
#endif

namespace ScottIsAFool.Windows.Controls
{
    public abstract class BasePage : Page
    {
        protected readonly ILog Logger;
        
#if WINDOWS_PHONE_APP
        protected virtual ApplicationViewBoundsMode Mode
        {
            get { return ApplicationViewBoundsMode.UseVisible; }
        }
#endif

        public abstract INavigationService NavigationService { get; }

        protected virtual NavigationCacheMode NavCacheMode
        {
            get { return NavigationCacheMode.Required; }
        }

        protected BasePage()
        {
            Logger = new WinLogger(GetType().FullName);
            Loaded += (sender, args) => NavigationCacheMode = NavCacheMode;
        }

        protected virtual void OnBackKeyPressed(object sender, NavigationServiceBackKeyPressedEventArgs e)
        {
        }

        [Conditional("WINDOWS_PHONE_APP")]
        protected void SetFullScreen()
        {
#if WINDOWS_PHONE_APP
            ApplicationView.GetForCurrentView().SetDesiredBoundsMode(Mode);
#endif
        }

        protected virtual void InitialiseOnBack()
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
#if WINDOWS_PHONE_APP
            if (NavigationService != null)
            {
                NavigationService.BackKeyPressed += OnBackKeyPressed;
            }
#endif

            Logger.Info("Navigated to {0}", GetType().FullName);

            SetFullScreen();

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

            var homeVm = DataContext as ICanHasHomeButton;
            if (homeVm != null)
            {
                homeVm.ShowHomeButton = parameters != null && parameters.ShowHomeButton;
            }

            var navigationVm = DataContext as INavigatedToViewModel;
            if (navigationVm != null)
            {
                navigationVm.NavigatedTo(e);
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
#if WINDOWS_PHONE_APP
            if (NavigationService != null)
            {
                NavigationService.BackKeyPressed -= OnBackKeyPressed;
            }
#endif

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

            var navigationVm = DataContext as INavigatedFromViewModel;
            if (navigationVm != null)
            {
                navigationVm.NavigatedFrom(e);
            }

            Logger.Info("Navigated from {0}", GetType().FullName);
            base.OnNavigatedFrom(e);
        }
    }
}
