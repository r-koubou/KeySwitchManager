using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;

namespace KeySwitchManager.UseCases.KeySwitches.Searching
{
    public class SearchingResponse
    {
        public IReadOnlyCollection<KeySwitch> Result { get; }
        public int FoundCount { get; }
        public IText Text { get; }

        public SearchingResponse( IReadOnlyCollection<KeySwitch> result, IText text )
        {
            Result     = result;
            FoundCount = Result.Count;
            Text       = text;
        }
    }
}