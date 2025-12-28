using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public abstract class ExportPathBuilder : IExportPathBuilder
    {
        public string Suffix { get; }
        public IDirectoryPath OutputDirectory { get; }

        protected ExportPathBuilder( string suffix ) : this( suffix, DirectoryPath.Default ) {}
        protected ExportPathBuilder( string suffix, IDirectoryPath outputDirectory )
        {
            Suffix          = suffix;
            OutputDirectory = outputDirectory;
        }

        public abstract IFilePath Build( IReadOnlyCollection<KeySwitch> keySwitches );
    }
}
