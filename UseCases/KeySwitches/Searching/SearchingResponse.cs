using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Aggregate;

namespace KeySwitchManager.UseCases.KeySwitches.Searching
{
    public class SearchingResponse
    {
        public IEnumerable<KeySwitch> Result { get; }
        public int FoundCount { get; }

        public SearchingResponse( IEnumerable<KeySwitch> result )
        {
            Result       = new List<KeySwitch>( result );
            FoundCount = Result.Count();
        }
    }
}