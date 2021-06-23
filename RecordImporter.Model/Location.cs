using RecordImporter.Model.Enums;

namespace RecordImporter.Model
{
    public class Location
    {
        public string PcPath { get; set; }
        public string DevicePath { get; set; }
        public string Action { get; set; }
        public TransferType TypeOfTransfer { get; set; }
        public bool AllFiles { get; set; }
        public string FileType { get; set; }
    }
}
