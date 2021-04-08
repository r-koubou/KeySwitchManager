using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;

namespace KeySwitchManager.UseCases.KeySwitches.Cakewalk.Exporting
{
    public class ExportingCakewalkArticulationResponse
    {
        public class Element
        {
            public KeySwitch KeySwitch { get; }
            public IText JsonText { get; }

            public Element( KeySwitch keySwitch, IText jsonText )
            {
                KeySwitch = keySwitch;
                JsonText  = jsonText;
            }
        }
        public bool Empty => !Elements.Any();

        public IReadOnlyCollection<Element> Elements { get; }

        public ExportingCakewalkArticulationResponse( IReadOnlyCollection<Element> elements )
        {
            Elements = elements;
        }
    }
}