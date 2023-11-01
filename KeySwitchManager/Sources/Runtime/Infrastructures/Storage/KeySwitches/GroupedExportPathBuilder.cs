using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class GroupedExportPathBuilder : ExportPathBuilder
    {
        protected GroupedExportPathBuilder( string suffix ) : base( suffix, DirectoryPath.Default ) {}

        protected GroupedExportPathBuilder( string suffix, IDirectoryPath outputDirectory ) : base( suffix, outputDirectory ) {}

        public override IFilePath Build( IReadOnlyCollection<KeySwitch> keySwitches )
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
