using Race.Windows.Ns.JsonSettings.Settings;

namespace RecordImporter.JsonSettings
{
    public class ProgramSettingsFileSystem : ISettings
    {
        public string DriveLetter { get; set; }
        public string PathOnPdaImport1 { get; set; }
        public string PathOnPdaExport { get; set; }
        public string PathOnDesktop1 { get; set; }
        public string PathOnDesktopExport { get; set; }
    }
}
