using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapiConnectivity
{
    public static class RapiDevices
    {
        public static List<RapiDevice> Devices = new List<RapiDevice>
        {
            new RapiDevice { Manufacturer = "Datalogic", Model = "Falcon X4", Platform = "WindowsCeGeneric" }

        };

        public static Task<List<string>> GetManufacturersAsync()
        {
            return Task.FromResult(Devices.Select(d => d.Manufacturer).ToList());
        }
    }
}
