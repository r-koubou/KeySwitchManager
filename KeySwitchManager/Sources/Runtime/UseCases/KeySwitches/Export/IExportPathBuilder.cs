using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportPathBuilder
    {
        DirectoryPath OutputDirectory { get; }
        string Suffix { get; }
        IFilePath Build( IReadOnlyCollection<KeySwitch> keySwitches );
    }
}
