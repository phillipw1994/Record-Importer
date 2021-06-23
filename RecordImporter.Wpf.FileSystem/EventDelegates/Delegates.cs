using System.Windows.Shell;
using RecordImporter.Wpf.FileSystem.Enums;

namespace RecordImporter.Wpf.FileSystem.EventDelegates
{
    public class Delegates
    {
        public delegate void NavigateEventArgs(object sender, AppPages page);
        public delegate void TaskbarIconUpdateEventArgs(object sender, TaskbarItemProgressState progressState, int progressValue);
    }
}
