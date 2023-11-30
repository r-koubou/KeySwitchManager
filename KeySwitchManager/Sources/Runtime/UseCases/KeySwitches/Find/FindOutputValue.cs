using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Find
{
    public sealed class FindOutputValue
    {
        public IReadOnlyCollection<KeySwitch> Result { get; }
        public int FoundCount { get; }

        public FindOutputValue( IReadOnlyCollection<KeySwitch> result )
        {
            Result     = result;
            FoundCount = Result.Count;
        }
    }
}
