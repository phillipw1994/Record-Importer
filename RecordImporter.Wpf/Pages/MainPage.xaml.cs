using System.Windows.Controls;
using RecordImporter.Wpf.Pages.Interfaces;
using RecordImporter.Wpf.Pages.ViewModels;
using RecordImporter.Wpf.Pages.ViewModels.Interfaces;

namespace RecordImporter.Wpf.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, IVmPage
    {
        public INavigationVm Vm { get; set; }

        public MainPage()
        {
            InitializeComponent();
            DataContext = Vm = new MainPageVm();
        }
    }
}
