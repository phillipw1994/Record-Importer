using System;
using System.Windows;
using Autofac;
using Race.Windows.Wpf.Base.Extensions;
using RecordImporter.Wpf.IocContainer;
using RecordImporter.Wpf.Windows;

namespace RecordImporter.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IContainer Container { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RunApplication();
        }

        private static async void RunApplication()
        {
            try
            {
                Container = RecordImporterContainer.Start();
                await new MainWindow().ShowDialogAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error Occured{Environment.NewLine}Message:{ex.Message}{Environment.NewLine}StackTrace:{ex.StackTrace}",
                    "", MessageBoxButton.OK, MessageBoxImage.Error);
                if (ex.InnerException != null)
                    MessageBox.Show(
                        $"Error Occured{Environment.NewLine}Inner Exception Message:{ex.InnerException.Message}{Environment.NewLine}Inner Exception StackTrace:{ex.InnerException.StackTrace}",
                        "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
