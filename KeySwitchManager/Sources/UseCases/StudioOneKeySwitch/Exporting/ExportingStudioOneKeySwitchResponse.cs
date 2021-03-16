using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;

namespace KeySwitchManager.UseCases.StudioOneKeySwitch.Exporting
{
    public class ExportingStudioOneKeySwitchResponse
    {
        public class Element
        {
            public KeySwitch KeySwitch { get; }
            public IText XmlText { get; }

            public Element( KeySwitch keySwitch, IText xmlText )
            {
                KeySwitch = keySwitch;
                XmlText      = xmlText;
            }
        }
        public bool Empty => !Elements.Any();

        public IReadOnlyCollection<Element> Elements { get; }

        public ExportingStudioOneKeySwitchResponse( IReadOnlyCollection<Element> elements )
        {
            Elements  = elements;
        }
    }
}