using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Models;

namespace KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Translators.Helpers
{
    internal static class KeySwitchToStudioOneModelHelper
    {
        public static RootElement Translate( KeySwitch source )
        {
            var rootElement = new RootElement
            {
                Name = $"{source.ProductName.Value} {source.InstrumentName.Value}"
            };

            foreach( var i in source.Articulations )
            {
                if( !i.MidiNoteOns.Any() )
                {
                    continue;
                }

                var attr = new ElementAttribute
                {
                    Name  = i.ArticulationName.Value,
                    Pitch = i.MidiNoteOns.First().DataByte1.ToString()!
                };
                rootElement.Attributes.Add( attr );
            }

            return rootElement;
        }
    }
}