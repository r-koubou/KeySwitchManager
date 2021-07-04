using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Find
{
    public class FindResponse
    {
        public IReadOnlyCollection<KeySwitch> Result { get; }
        public int FoundCount { get; }

        public FindResponse( IReadOnlyCollection<KeySwitch> result )
        {
            Result     = result;
            FoundCount = Result.Count;
        }
    }
}