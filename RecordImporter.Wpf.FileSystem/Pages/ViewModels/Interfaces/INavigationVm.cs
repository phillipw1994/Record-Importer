using System.Threading.Tasks;
using RecordImporter.Wpf.FileSystem.EventDelegates;

namespace RecordImporter.Wpf.FileSystem.Pages.ViewModels.Interfaces
{
    public interface INavigationVm
    {
        event Delegates.NavigateEventArgs Navigate;
        event Delegates.TaskbarIconUpdateEventArgs TaskbarIconUpdate;

        Task LoadPageAsync();
    }
}
