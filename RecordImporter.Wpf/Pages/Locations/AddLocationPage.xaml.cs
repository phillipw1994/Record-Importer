using System.Windows;
using System.Windows.Controls;
using Race.Windows.Wpf.Ui.Dialogs;

namespace RecordImporter.Wpf.Pages.Locations
{
    /// <summary>
    /// Interaction logic for AddLocation.xaml
    /// </summary>
    public partial class AddLocationPage : Page
    {
        public AddLocationPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var select = new SelectLocalLocationWindow();
            select.ShowDialog();
        }
    }
}
