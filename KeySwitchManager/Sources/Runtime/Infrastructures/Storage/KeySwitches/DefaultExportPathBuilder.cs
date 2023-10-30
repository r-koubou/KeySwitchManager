using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public sealed class DefaultExportPathBuilder : IExportPathBuilder
    {
        public string Suffix { get; }
        public IDirectoryPath OutputDirectory { get; }

        public DefaultExportPathBuilder( string suffix, IDirectoryPath outputDirectory )
        {
            Suffix          = suffix;
            OutputDirectory = outputDirectory;
        }

        public IFilePath Build( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            return CreatePathHelper.CreateFilePath( keySwitches.First(), Suffix, OutputDirectory );
        }
    }
}
