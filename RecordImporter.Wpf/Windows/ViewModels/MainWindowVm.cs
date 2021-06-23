using System;
using System.Windows;
using System.Windows.Controls;
using Race.Windows.Wpf.Base;
using RecordImporter.Wpf.Enums;
using RecordImporter.Wpf.Pages;
using RecordImporter.Wpf.Pages.Interfaces;
using RecordImporter.Wpf.Pages.ViewModels.Interfaces;

namespace RecordImporter.Wpf.Windows.ViewModels
{
    public class MainWindowVm : WindowBaseViewModel
    {
        #region Binding Properties

        public Page CurrentPage { get; set; }

        #endregion

        #region Private Properties

        private MainPage MainPage { get; }
        private SettingsPage SettingsPage { get; }
        private INavigationVm CurrentViewModel { get; set; } 
        
        #endregion

        public MainWindowVm(Window parentWindow) : base(parentWindow)
        {
            //parentWindow.MainContainer.Child = new MainPage();
            MainPage = new MainPage();
            SettingsPage = new SettingsPage();
            CurrentPage = SettingsPage;
            CurrentViewModel = ((IVmPage)CurrentPage).Vm;
            CurrentViewModel.Navigate += CurrentViewModel_Navigate;
            PortableDeviceDataSync PortableDeviceDataSync = new PortableDeviceDataSync(null);
        }

        private void CurrentViewModel_Navigate(object sender, AppPages page)
        {
            DeRegisterEvents();
            switch (page)
            {
                case AppPages.Main:
                    CurrentPage = MainPage;
                    CurrentViewModel = ((IVmPage)CurrentPage).Vm;
                    CurrentViewModel.Navigate += CurrentViewModel_Navigate;
                    break;
                case AppPages.Settings:
                    CurrentPage = SettingsPage;
                    CurrentViewModel = ((IVmPage)CurrentPage).Vm;
                    CurrentViewModel.Navigate += CurrentViewModel_Navigate;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, null);
            }
        }

        private void DeRegisterEvents()
        {
            if (CurrentPage == null) return;
            var vm = ((IVmPage) CurrentPage).Vm;
            vm.Navigate -= CurrentViewModel_Navigate;
            CurrentPage = null;
            CurrentViewModel = null;
        }
    }
}
