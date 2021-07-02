using System.IO;
using System.Reflection;
using System.Text;

using Application.Core.Controllers.Export;

using KeySwitchManager.Domain.KeySwitches.Models.Values;

using YamlDotNet.Serialization;

namespace KeySwitchManager.WPF
{
    public class ApplicationConfigModel
    {
        public string ImportDatabasePath { get; set; } = string.Empty;
        public string ExportDatabasePath { get; set; } = string.Empty;
        public string FindDeveloperName { get; set; } = DeveloperName.Any.Value;
        public string FindProductName { get; set; } = ProductName.Any.Value;
        public string FindInstrumentName { get; set; } = InstrumentName.Any.Value;
        public string ExportDirectory { get; set; } = string.Empty;
        public string ExportFormat { get; set; } = ExportSupportedFormat.Xlsx.ToString();
    }

    public class ApplicationConfig
    {
        private static string ApplicationDirectory =>
            Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location
            );

        private static string ConfigFilePath =>
            Path.Combine( ApplicationDirectory, "config.yaml" );

        private static readonly Encoding Encoding = Encoding.UTF8;

        public static ApplicationConfigModel Load()
        {
            try
            {
                var deserializer = new DeserializerBuilder().Build();
                var model = deserializer.Deserialize<ApplicationConfigModel>(
                    File.ReadAllText( ConfigFilePath, Encoding )
                );

                return model;
            }
            catch
            {
                return new ApplicationConfigModel();
            }
        }

        public static void Save( ApplicationConfigModel config )
        {
            try
            {
                var appDir = Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location
                );

                var sb = new StringBuilder();
                using var writer = new StringWriter( sb );

                var serializer = new Serializer();
                serializer.Serialize( writer, config );

                File.WriteAllText( ConfigFilePath, sb.ToString(), Encoding );
            }
            catch
            {
                // ignored
            }
        }
    }
}
