using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Autofac;
using Race.Windows.Ns.JsonSettings.Managers;
using RecordImporter.JsonSettings;

namespace RecordImporter.Wpf.IocContainer
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

            if(!File.Exists(Path.Combine(path, "appsettings.json")))
                File.Create(Path.Combine(path, "appsettings.json")).Close();

            var builder = new ContainerBuilder();

            builder.RegisterType<SettingsManagerJson<ProgramSettings>>()
                .As<ISettingsManager<ProgramSettings>>()
                .InstancePerDependency();

            builder.Register<ISettingsManager<ProgramSettings>>(c => new SettingsManagerJson<ProgramSettings>(Path.Combine(path, "appsettings.json")));

            
            return builder.Build();
        }
    }
}
