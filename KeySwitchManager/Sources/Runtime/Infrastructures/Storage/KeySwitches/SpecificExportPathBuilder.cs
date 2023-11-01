using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class SpecificExportPathBuilder : IExportPathBuilder
    {
        private IFilePath FilePath { get; }
        // no use
        public string Suffix => string.Empty;

        // no use
        public IDirectoryPath OutputDirectory { get; } = DirectoryPath.Default;

        public SpecificExportPathBuilder( IFilePath filePath )
        {
            FilePath = filePath;
        }

        public IFilePath Build( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            return FilePath;
        }
    }
}
