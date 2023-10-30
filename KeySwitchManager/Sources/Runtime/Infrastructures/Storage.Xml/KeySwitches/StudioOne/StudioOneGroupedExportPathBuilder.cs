using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne
{
    public sealed class StudioOneGroupedExportPathBuilder : IExportPathBuilder
    {
        public string Suffix => ".keyswitch";
        public IDirectoryPath OutputDirectory { get; }

        public StudioOneGroupedExportPathBuilder( IDirectoryPath outputDirectory )
        {
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
