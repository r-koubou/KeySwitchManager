using System.Linq;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.UseCases.KeySwitches.StudioOne.Translators;
using KeySwitchManager.Xml.KeySwitches.StudioOne.Models;

using RkHelper.Text.Xml;

namespace KeySwitchManager.Xml.KeySwitches.StudioOne.Translators
{
    public class KeySwitchToStudioOneKeySwitchModel : IKeySwitchToStudioOneKeySwitchModel
    {
        public IText Translate( Domain.KeySwitches.KeySwitch source )
        {
            var xml = new RootElement
            {
                Name = $"{source.ProductName.Value} {source.InstrumentName.Value}"
            };

            foreach( var i in source.Articulations )
            {
                if( !i.MidiNoteOns.Any() )
                {
                    continue;
                }

                var attr = new Attributes
                {
                    Name  = i.ArticulationName.Value,
                    Pitch = i.MidiNoteOns.First().DataByte1.ToString()!
                };
                xml.Attributes.Add( attr );
            }

            return new PlainText( XmlHelper.ToXmlString( xml ) );
        }
    }
}