using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;

namespace ScottIsAFool.Windows.Core.Services
{
    public interface ICameraInfoService
    {
        Task StartService(MediaCaptureInitializationSettings settings);
        Task StartService(CameraType cameraType = CameraType.Primary);
        Task<bool> HasPrimaryCamera();
        Task<bool> HasFrontFacingCamera();
        Task<bool> HasFlash();
        Task<bool> SupportsFocus();
        Task<DeviceInformation> GetDevice(CameraType cameraType);
        MediaCapture MediaCapture { get; }
        bool IsInitialised { get; }
        Task DisposeMediaCapture();
        Task StartPreview(Task preStart = null);
        Task StopPreview();
        event EventHandler IsInitialisedChanged;
    }

    public enum CameraType
    {
        FrontFacing,
        Primary
    }
}