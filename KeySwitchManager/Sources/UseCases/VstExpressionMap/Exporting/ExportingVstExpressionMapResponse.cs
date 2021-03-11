using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;

namespace KeySwitchManager.UseCases.VstExpressionMap.Exporting
{
    public class ExportingVstExpressionMapResponse
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

        public ExportingVstExpressionMapResponse( IReadOnlyCollection<Element> elements )
        {
            Elements  = elements;
        }
    }
}