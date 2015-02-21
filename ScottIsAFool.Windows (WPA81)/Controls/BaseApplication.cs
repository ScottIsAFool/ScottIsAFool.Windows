using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace ScottIsAFool.Windows.Controls
{
    public class BaseApplication : Application
    {
        /// <summary>
        /// This method is called at the start of every method called by
        /// an app's launch/activation. Any services that should be started
        /// when the app starts should be put in here.
        /// </summary>
        public virtual void AppStarted() { }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            AppStarted();
            base.OnActivated(args);
        }

        protected override void OnCachedFileUpdaterActivated(CachedFileUpdaterActivatedEventArgs args)
        {
            AppStarted();
            base.OnCachedFileUpdaterActivated(args);
        }

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            AppStarted();
            base.OnFileActivated(args);
        }

        protected override void OnFileOpenPickerActivated(FileOpenPickerActivatedEventArgs args)
        {
            AppStarted();
            base.OnFileOpenPickerActivated(args);
        }

        protected override void OnFileSavePickerActivated(FileSavePickerActivatedEventArgs args)
        {
            AppStarted();
            base.OnFileSavePickerActivated(args);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            AppStarted();
            base.OnLaunched(args);
        }

        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            AppStarted();
            base.OnSearchActivated(args);
        }

        protected override void OnShareTargetActivated(ShareTargetActivatedEventArgs args)
        {
            AppStarted();
            base.OnShareTargetActivated(args);
        }
    }
}
