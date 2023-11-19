using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export
{
    public sealed class ClosedXmlGroupedExportPathBuilder : ExportPathBuilder
    {
        public ClosedXmlGroupedExportPathBuilder() : this( DirectoryPath.Default ) {}

        public ClosedXmlGroupedExportPathBuilder( IDirectoryPath outputDirectory ) : base( ".xlsx", outputDirectory ) {}

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
