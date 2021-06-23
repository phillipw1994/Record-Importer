using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using RecordImporter.Wpf.Windows.ViewModels;

namespace RecordImporter.Wpf.Windows
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
            DataContext = new DialogWindowVm(this, null);
        }
        public DialogWindow(Page page)
        {
            InitializeComponent(); 
            DataContext = new DialogWindowVm(this, page);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, TimeSpan.FromMilliseconds(400));
            anim.Completed += (s, _) => Close();
            BeginAnimation(OpacityProperty, anim);
        }
    }
}
