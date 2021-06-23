using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Autofac;
using Race.Windows.Ns.JsonSettings.Managers;
using RecordImporter.JsonSettings;

namespace RecordImporter.Wpf.FileSystem.IocContainer
{
    public class RecordImporterContainer
    {
        public static IContainer Start()
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location ?? throw new InvalidOperationException());

            var companyName = versionInfo.CompanyName;
            var productName = versionInfo.ProductName;
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), companyName, productName);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(Path.Combine(path, "appsettings.json")))
            {
                ISettingsManager<ProgramSettingsFileSystem> settingsManager =
                    new SettingsManagerJson<ProgramSettingsFileSystem>(Path.Combine(path, "appsettings.json"));
                var settings = new ProgramSettingsFileSystem
                {
                    PathOnDesktop1 = @"C:\Orders\ToDevice\",
                    PathOnDesktopExport = @"C:\Orders\FromDevice\",
                    DriveLetter = @"D:\",
                    PathOnPdaImport1 = @"Race_Software\AltoPlastics\Import\",
                    PathOnPdaExport = @"Race_Software\AltoPlastics\Export\"
                };
                settingsManager.Save(settings);
            }

            var builder = new ContainerBuilder();

            builder.RegisterType<SettingsManagerJson<ProgramSettingsFileSystem>>()
                .As<ISettingsManager<ProgramSettingsFileSystem>>()
                .InstancePerLifetimeScope();

            builder.Register<ISettingsManager<ProgramSettingsFileSystem>>(c => new SettingsManagerJson<ProgramSettingsFileSystem>(Path.Combine(path, "appsettings.json")));

            return builder.Build();
        }
    }
}
