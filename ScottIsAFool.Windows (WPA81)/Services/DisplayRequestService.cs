using Windows.System.Display;
using ScottIsAFool.Windows.Core.Services;

namespace ScottIsAFool.Windows.Services
{
    public class DisplayRequestService : IDisplayRequestService
    {
        private DisplayRequest _displayRequest;

        public DisplayRequestService()
        {
            _displayRequest = new DisplayRequest();
        }

        public bool IsActive { get; private set; }

        public void Request()
        {
            if (_displayRequest == null)
            {
                _displayRequest = new DisplayRequest();
            }

            _displayRequest.RequestActive();
            IsActive = true;
        }

        public void Release()
        {
            if (_displayRequest == null || !IsActive)
            {
                return;
            }

            _displayRequest.RequestRelease();
            _displayRequest = null;
            IsActive = false;
        }
    }
}
