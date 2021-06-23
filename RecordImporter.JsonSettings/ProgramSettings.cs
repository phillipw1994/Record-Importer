using System.Collections.Generic;
using Race.Windows.Ns.JsonSettings.Settings;

namespace RecordImporter.JsonSettings
{
    public class ProgramSettings: ISettings
    {
        public List<Location> Locations { get; set; }
        public string Device { get; set; }
        public string DeviceOs { get; set; }
        public string StorageFolder { get; set; }
        public string ProductFolder { get; set; }
    }
}
