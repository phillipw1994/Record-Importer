using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Race.Windows.Ns.JsonSettings.Managers;
using Race.Windows.Wpf.Base;
using RecordImporter.JsonSettings;
using RecordImporter.Model;
using RecordImporter.Wpf.Enums;
using RecordImporter.Wpf.EventDelegates;
using RecordImporter.Wpf.Pages.Locations;
using RecordImporter.Wpf.Pages.ViewModels.Interfaces;
using RecordImporter.Wpf.Windows;

namespace RecordImporter.Wpf.Pages.ViewModels
{
    public class SettingsPageVm : BaseViewModel, INavigationVm
    {
        #region Injected Properties

        private ISettingsManager<ProgramSettings> SettingsManager { get; }

        #endregion

        #region Private Properties

        private ProgramSettings Settings { get; set; }

        #endregion

        #region Public Properties

        public event Delegates.NavigateEventArgs Navigate;
        public ICommand CancelButtonCommand { get; }
        public ICommand SaveButtonCommand { get; }
        public ICommand AddButtonCommand { get; }
        public ICommand RefreshDevicesCommand { get; }
        public ICommand EditButtonCommand { get; }
        public ICommand DeleteButtonCommand { get; }
        public ICommand ExportLocationButtonCommand { get; }
        public ICommand ImportLocationButtonCommand { get; }

        private PortableDeviceDataSync Pdds { get; }

        public BindingList<Location> Locations { get; set; }
        public BindingList<string> ConnectedDevices { get; set; }
        public BindingList<string> DeviceOsList { get; set; }
        public BindingList<string> StorageFolders { get; set; }

        private string _selectedDevice;
        public string SelectedDevice
        {
            get => _selectedDevice;
            set
            {
                _selectedDevice = value;
                LoadStorageFolders();
            }
        }

        private void LoadStorageFolders()
        {
            StorageFolders.Clear();
            var folders = Pdds.GetStorageFolders(SelectedDevice);
            if(folders == null) return;
            foreach (var folder in folders)
            {
                StorageFolders.Add(folder);
            }
            StorageFolders.ResetBindings();
        }

        public string RaceSoftwareFolder { get; set; } = "Race_Software";

        public string SelectedDeviceOs { get; set; }
        public string SelectedStorageFolder { get; set; }
        public string SoftwareProductFolder { get; set; }

        #endregion

        #region Constructors

        public SettingsPageVm(ISettingsManager<ProgramSettings> settingsManager)
        {
            //Setup Injected properties
            SettingsManager = settingsManager;

            //Setup Lists
            ConnectedDevices = new BindingList<string>();
            DeviceOsList = new BindingList<string>();
            StorageFolders = new BindingList<string>();
            Locations = new BindingList<Location>();
            Pdds = new PortableDeviceDataSync(new List<Location>());

            //Setup Buttons
            CancelButtonCommand = new RelayCommand(async param => await CancelButtonCommandAsync(), param => true);
            SaveButtonCommand = new RelayCommand(async param => await SaveButtonCommandAsync(), param => true);
            AddButtonCommand = new RelayCommand(async param => await AddButtonCommandAsync(), param => true);
            RefreshDevicesCommand =
                new RelayCommand(async param => await RefreshConnectedDevicesAsync(), param => true);
        } 
        
        #endregion

        #region Button Commands

        private Task SaveButtonCommandAsync()
        {
            if (string.IsNullOrEmpty(SelectedDevice) || string.IsNullOrEmpty(SelectedDeviceOs) ||
                string.IsNullOrEmpty(SelectedStorageFolder) || string.IsNullOrEmpty(RaceSoftwareFolder) ||
                string.IsNullOrEmpty(SoftwareProductFolder))
                return Task.CompletedTask;
            Settings.Device = SelectedDevice;
            Settings.DeviceOs = SelectedDeviceOs;
            Settings.StorageFolder = SelectedStorageFolder;
            Settings.ProductFolder = SoftwareProductFolder;
            SettingsManager.Save(Settings);
            return Task.CompletedTask;
        }

        private Task CancelButtonCommandAsync()
        {
            Navigate?.Invoke(this, AppPages.Main);
            return Task.CompletedTask;
        }

        private Task AddButtonCommandAsync()
        {
            var page = new AddLocationPage();
            var dw = new DialogWindow(page);
            dw.ShowDialog();
            return Task.CompletedTask;
        }

        private Task RefreshConnectedDevicesAsync()
        {
            ConnectedDevices.Clear();
            var devices = Pdds.GetDevices();

            foreach (var device in devices)
            {
                ConnectedDevices.Add(string.IsNullOrEmpty(device.DeviceFriendlyName) ? $"{device.DeviceDescription}" : $"{device.DeviceFriendlyName}");
            }
            ConnectedDevices.ResetBindings();
            return Task.CompletedTask;
        } 

        #endregion

        #region Load Data

        public async Task LoadPageAsync()
        {
            await LoadSettingsAsync();
            await LoadLocationsAsync();
            await LoadDevicesAsync();
            await LoadDeviceOsAsync();
        }

        private Task LoadDeviceOsAsync()
        {
            DeviceOsList.Add("Android");
            DeviceOsList.Add("Windows CE");
            DeviceOsList.Add("Windows Mobile");
            DeviceOsList.ResetBindings();

            return Task.CompletedTask;
        }

        private Task LoadDevicesAsync()
        {
            var devices = Pdds.GetDevices();
            var devicesList = devices.ToList();
            devicesList.ForEach(d => ConnectedDevices.Add(string.IsNullOrEmpty(d.DeviceFriendlyName)
                ? $"{d.DeviceDescription}"
                : $"{d.DeviceFriendlyName}"));
            ConnectedDevices.ResetBindings();

            return Task.CompletedTask;
        }

        private Task LoadLocationsAsync()
        {
            Settings.Locations.ForEach(l => Locations.Add(l));
            Locations.ResetBindings();
            return Task.CompletedTask;
        }

        private Task LoadSettingsAsync()
        {
            var settings = SettingsManager.Load();
            if (settings != null)
            {
                SelectedDevice = settings.Device;
                SelectedDeviceOs = settings.DeviceOs;
                SelectedStorageFolder = settings.StorageFolder;
                SoftwareProductFolder = settings.ProductFolder;
            }
            else
                settings = new ProgramSettings { Locations = new List<Location>() };

            Settings = settings;
            return Task.CompletedTask;
        } 

        #endregion
    }
}