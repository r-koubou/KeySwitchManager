using System.Linq;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.UseCases.StudioOneKeySwitch.Translations;
using KeySwitchManager.Xml.StudioOne.KeySwitch.Models;

using RkHelper.Text.Xml;

namespace KeySwitchManager.Xml.StudioOne.KeySwitch.Translations
{
    public class KeySwitchToStudioOneKeySwitchModel : IKeySwitchToStudioOneKeySwitchModel
    {
        public IText Translate( Domain.KeySwitches.Aggregate.KeySwitch source )
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