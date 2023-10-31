using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export
{
    public sealed class YamlGroupedExportPathBuilder : IExportPathBuilder
    {

        public string Suffix { get; }
        public IDirectoryPath OutputDirectory { get; }

        public YamlGroupedExportPathBuilder( IDirectoryPath outputDirectory ) : this( ".yaml", outputDirectory ) {}

        public YamlGroupedExportPathBuilder( string suffix, IDirectoryPath outputDirectory )
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
