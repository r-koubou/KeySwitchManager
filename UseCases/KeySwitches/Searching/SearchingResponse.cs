using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Aggregate;

namespace KeySwitchManager.UseCases.KeySwitches.Searching
{
    public class SearchingResponse
    {
        public IReadOnlyCollection<KeySwitch> Result { get; }
        public int FoundCount { get; }

        public SearchingResponse( IReadOnlyCollection<KeySwitch> result )
        {
            Result     = result;
            FoundCount = Result.Count();
        }
    }
}