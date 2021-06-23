using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;
using Race.Windows.Ns.JsonSettings.Managers;
using Race.Windows.Wpf.Base;
using RecordImporter.JsonSettings;
using RecordImporter.Wpf.FileSystem.Enums;
using RecordImporter.Wpf.FileSystem.Pages;
using RecordImporter.Wpf.FileSystem.Pages.Interfaces;
using RecordImporter.Wpf.FileSystem.Pages.ViewModels.Interfaces;

namespace RecordImporter.Wpf.FileSystem.Windows.ViewModels
{
    public class MainWindowVm : WindowBaseViewModel
    {
        #region Binding Properties

        public Page CurrentPage { get; set; }
        public TaskbarItemProgressState ProgressState { get; set; }
        public int ProgressValue { get; set; }

        #endregion

        #region Private Properties

        private MainPage MainPage { get; }
        private INavigationVm CurrentViewModel { get; set; } 
        
        #endregion

        public MainWindowVm(Window parentWindow, ISettingsManager<ProgramSettingsFileSystem> settingsManager) : base(parentWindow)
        {
            //parentWindow.MainContainer.Child = new MainPage();
            MainPage = new MainPage(settingsManager);
            CurrentPage = MainPage;
            CurrentViewModel = ((IVmPage)CurrentPage).Vm;
            CurrentViewModel.TaskbarIconUpdate += CurrentViewModel_TaskbarIconUpdate;
            CurrentViewModel.Navigate += CurrentViewModel_Navigate;
            ProgressState = TaskbarItemProgressState.Error;
            ProgressValue = 100;
        }

        private void CurrentViewModel_TaskbarIconUpdate(object sender, TaskbarItemProgressState progressState, int progressValue)
        {
            ProgressState = progressState;
            ProgressValue = progressValue;
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
