using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class ExportFileResponse
    {
        public IReadOnlyCollection<KeySwitch> Result { get; }
        public int WriteCount { get; }

        public ExportFileResponse( IReadOnlyCollection<KeySwitch> result )
        {
            Result     = result;
            WriteCount = Result.Count;
        }
    }
}