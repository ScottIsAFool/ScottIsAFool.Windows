﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Capture;
using ScottIsAFool.Windows.Core.Services;

namespace ScottIsAFool.Windows.Services
{
    public class CameraInfoService : ICameraInfoService
    {
        private static DeviceInformationCollection _deviceCollection;
        private static MediaCapture _captureManager; 
        
        private bool _previewStarted;
        
        public CameraInfoService()
        {
            _captureManager = new MediaCapture();
        }

        public MediaCapture MediaCapture
        {
            get { return _captureManager; }
        }

        public bool IsInitialised { get; private set; }

        public async Task<DeviceInformation> GetDevice(CameraType cameraType)
        {
            if (_deviceCollection == null)
            {
                _deviceCollection = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);
            }

            var panel = cameraType == CameraType.FrontFacing ? Panel.Front : Panel.Back;

            var device = _deviceCollection.FirstOrDefault(x => x.EnclosureLocation != null && x.EnclosureLocation.Panel == panel);

            return device;
        }
        
        public async Task StartPreview(Task preStart = null)
        {
            if (_captureManager == null)
            {
                _captureManager = new MediaCapture();
            }

            if (IsInitialised)
            {
                if (preStart != null)
                {
                    await preStart;
                }

                if (!_previewStarted)
                {
                    await _captureManager.StartPreviewAsync();
                    _previewStarted = true;
                }
            }
        }

        public async Task StopPreview()
        {
            if (_captureManager == null)
            {
                return;
            }

            if (_previewStarted)
            {
                await _captureManager.StopPreviewAsync();
                _previewStarted = false;
            }
        }

        public event EventHandler IsInitialisedChanged;

        public async Task DisposeMediaCapture()
        {
            if (_captureManager == null)
            {
                return;
            }

            await StopPreview();

            _captureManager.Dispose();
            _captureManager = null;
            IsInitialised = false;
            InitialisedChanged();
        }

        public async Task StartService(CameraType cameraType = CameraType.Primary)
        {
            var device = await GetDevice(cameraType);
            if (device == null)
            {
                await StartService(null);
            }
            else
            {
                await StartService(new MediaCaptureInitializationSettings {VideoDeviceId = device.Id});
            }
        }
       
        public async Task StartService(MediaCaptureInitializationSettings settings)
        {
            try
            {
                if (_captureManager == null)
                {
                    _captureManager = new MediaCapture();
                }

                if (!IsInitialised)
                {
                    IsInitialised = true;
                    if (settings == null)
                    {
                        var device = await GetDevice(CameraType.Primary);
                        if (device != null)
                        {
                            await _captureManager.InitializeAsync(new MediaCaptureInitializationSettings {VideoDeviceId = device.Id});
                        }
                        else
                        {
                            await _captureManager.InitializeAsync();
                        }
                    }
                    else
                    {
                        await _captureManager.InitializeAsync(settings);
                    }
                    InitialisedChanged();
                }
            }
            catch { }
        }

        public async Task<bool> HasPrimaryCamera()
        {
            return await HasCameraType(CameraType.Primary);
        }

        public async Task<bool> HasFrontFacingCamera()
        {
            return await HasCameraType(CameraType.FrontFacing);
        }

        public async Task<bool> HasFlash()
        {
            return _captureManager.VideoDeviceController != null
                   && _captureManager.VideoDeviceController.FlashControl != null
                   && _captureManager.VideoDeviceController.FlashControl.Supported;
        }

        public async Task<bool> SupportsFocus()
        {
            return _captureManager.VideoDeviceController != null
                   && _captureManager.VideoDeviceController.FocusControl != null
                   && _captureManager.VideoDeviceController.FocusControl.Supported;
        }

        private async Task<bool> HasCameraType(CameraType cameraType)
        {
            var device = await GetDevice(cameraType);
            return device != null;
        }

        private void InitialisedChanged()
        {
            var changed = IsInitialisedChanged;
            if (changed != null)
            {
                changed(this, EventArgs.Empty);
            }
        }

    }
}