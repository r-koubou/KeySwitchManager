using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public sealed class DefaultExportPathBuilder : ExportPathBuilder
    {
        public DefaultExportPathBuilder( string suffix = "" ) : this( suffix, DirectoryPath.Default ) {}

        public DefaultExportPathBuilder( string suffix, IDirectoryPath outputDirectory ) : base( suffix, outputDirectory ) {}

        public override IFilePath Build( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            return CreatePathHelper.CreateFilePath( keySwitches.First(), Suffix, OutputDirectory );
        }
    }
}
