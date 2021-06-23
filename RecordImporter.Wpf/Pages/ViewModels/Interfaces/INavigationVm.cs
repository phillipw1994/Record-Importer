using System.Threading.Tasks;
using RecordImporter.Wpf.EventDelegates;

namespace RecordImporter.Wpf.Pages.ViewModels.Interfaces
{
    public interface INavigationVm
    {
        event Delegates.NavigateEventArgs Navigate;
        Task LoadPageAsync();
    }
}
