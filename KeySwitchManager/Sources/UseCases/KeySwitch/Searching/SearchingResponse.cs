using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.UseCases.KeySwitch.Searching
{
    public class SearchingResponse
    {
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Result { get; }
        public int FoundCount { get; }
        public IText Text { get; }

        public SearchingResponse( IReadOnlyCollection<Domain.KeySwitches.KeySwitch> result, IText text )
        {
            Result     = result;
            FoundCount = Result.Count;
            Text       = text;
        }
    }
}