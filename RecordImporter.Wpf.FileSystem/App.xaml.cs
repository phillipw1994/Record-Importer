using System;
using System.Windows;
using Autofac;
using Race.Windows.Ns.JsonSettings.Managers;
using Race.Windows.Wpf.Base.Extensions;
using RecordImporter.JsonSettings;
using RecordImporter.Wpf.FileSystem.IocContainer;
using RecordImporter.Wpf.FileSystem.Windows;

namespace RecordImporter.Wpf.FileSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RunApplication();
        }

        private static async void RunApplication()
        {
            try
            {
                using (var scope = RecordImporterContainer.Start().BeginLifetimeScope())
                {
                    await new MainWindow(scope.Resolve<ISettingsManager<ProgramSettingsFileSystem>>()).ShowDialogAsync();
                }
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
