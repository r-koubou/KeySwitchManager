using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public sealed class YamlGroupedExportPathBuilder : IExportPathBuilder
    {

        public string Suffix { get; }
        public DirectoryPath OutputDirectory { get; }

        public YamlGroupedExportPathBuilder( DirectoryPath outputDirectory ) : this( ".yaml", outputDirectory ) {}

        public YamlGroupedExportPathBuilder( string suffix, DirectoryPath outputDirectory )
        {
            Suffix          = suffix;
            OutputDirectory = outputDirectory;
        }

        public IFilePath Build( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            var source = keySwitches.First();
            var developer = source.DeveloperName;
            var product = source.ProductName;

            return CreatePathHelper.CreateFilePath(
                developerName: developer,
                productName: product,
                prefix: "",
                suffix: Suffix,
                OutputDirectory
            );
        }
    }
}
