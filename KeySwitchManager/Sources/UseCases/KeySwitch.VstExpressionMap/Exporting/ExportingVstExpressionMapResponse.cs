using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.UseCases.KeySwitch.VstExpressionMap.Exporting
{
    public class ExportingVstExpressionMapResponse
    {
        public class Element
        {
            public Domain.KeySwitches.KeySwitch KeySwitch { get; }
            public IText XmlText { get; }

            public Element( Domain.KeySwitches.KeySwitch keySwitch, IText xmlText )
            {
                KeySwitch = keySwitch;
                XmlText      = xmlText;
            }
        }
        public bool Empty => !Elements.Any();

        public IReadOnlyCollection<Element> Elements { get; }

        public ExportingVstExpressionMapResponse( IReadOnlyCollection<Element> elements )
        {
            Elements  = elements;
        }
    }
}