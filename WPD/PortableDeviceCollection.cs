using System.Collections.ObjectModel;
using System.Linq;
using PortableDeviceApiLib;

namespace PortableDevices
{
    public class PortableDeviceCollection : Collection<PortableDevice>
    {
        private readonly PortableDeviceManager _deviceManager;

        public PortableDeviceCollection()
        {
            _deviceManager = new PortableDeviceManager();
        }

        public void Refresh()
        {
            _deviceManager.RefreshDeviceList();

            // Determine how many WPD devices are connected
            //var deviceIds = new string[1];
            uint count = 1;
            _deviceManager.GetDevices(null, ref count);

            if (count == 0) return;
            // Retrieve the device id for each connected device
            var deviceIds = new string[count];
            _deviceManager.GetDevices(deviceIds, ref count);
            foreach (var deviceId in deviceIds)
            {
                Add(new PortableDevice(deviceId, GetDeviceFriendlyName(deviceId), GetDeviceDescription(deviceId), GetDeviceManufacturer(deviceId)));
            }
        }

        private string GetDeviceDescription(string deviceId)
        {
            try
            {
                uint cDeviceDescription = 0;
                var strDeviceDescription = string.Empty;

                // First, pass NULL as the LPWSTR return string parameter to get the total number
                // of characters to allocate for the string value.
                _deviceManager.GetDeviceDescription(deviceId, null, ref cDeviceDescription);

                // Second allocate the number of characters needed and retrieve the string value.

                var usDeviceDescription = new ushort[cDeviceDescription];
                _deviceManager.GetDeviceDescription(deviceId, usDeviceDescription, ref cDeviceDescription);

                // We need to convert the array of ushorts to a string, one
                // character at a time.
                return usDeviceDescription.Where(letter => letter != 0).Aggregate(strDeviceDescription, (current, letter) => current + (char)letter);
            }
            catch
            {
                return string.Empty;
            }
        }

        private string GetDeviceFriendlyName(string deviceId)
        {
            try
            {
                uint cFriendlyName = 0;
                var strFriendlyName = string.Empty;

                // First, pass NULL as the LPWSTR return string parameter to get the total number
                // of characters to allocate for the string value.
                _deviceManager.GetDeviceFriendlyName(deviceId, null, ref cFriendlyName);

                // Second allocate the number of characters needed and retrieve the string value.

                var usFriendlyName = new ushort[cFriendlyName];
                _deviceManager.GetDeviceFriendlyName(deviceId, usFriendlyName, ref cFriendlyName);

                // We need to convert the array of ushorts to a string, one
                // character at a time.
                return usFriendlyName.Where(letter => letter != 0).Aggregate(strFriendlyName, (current, letter) => current + (char)letter);
            }
            catch
            {
                return string.Empty;
            }
        }

        private string GetDeviceManufacturer(string deviceId)
        {
            try
            {
                uint cDeviceManufacturer = 0;
                var strDeviceManufacturer = string.Empty;

                // First, pass NULL as the LPWSTR return string parameter to get the total number
                // of characters to allocate for the string value.
                _deviceManager.GetDeviceManufacturer(deviceId, null, ref cDeviceManufacturer);

                // Second allocate the number of characters needed and retrieve the string value.

                var usDeviceManufacturer = new ushort[cDeviceManufacturer];
                _deviceManager.GetDeviceManufacturer(deviceId, usDeviceManufacturer, ref cDeviceManufacturer);

                // We need to convert the array of ushorts to a string, one
                // character at a time.
                return usDeviceManufacturer.Where(letter => letter != 0).Aggregate(strDeviceManufacturer, (current, letter) => current + (char)letter);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}