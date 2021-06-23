using System.Windows;
using System.Windows.Controls;
using Race.Windows.Wpf.Base;

namespace RecordImporter.Wpf.FileSystem.Windows.ViewModels
{
    public class DialogWindowVm : WindowBaseViewModel
    {
        #region Binding Properties

        public Page CurrentPage { get; set; }

        #endregion

        public DialogWindowVm(Window parentWindow, Page page) : base(parentWindow)
        {
            CurrentPage = page;
        }
    }
}
