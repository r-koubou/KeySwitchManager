using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitch.Cakewalk.Model;

using Articulation = KeySwitchManager.Domain.KeySwitches.Entity.Articulation;
using CwArticulation = KeySwitchManager.Json.KeySwitch.Cakewalk.Model.Articulation;

namespace KeySwitchManager.Json.KeySwitch.Cakewalk.Translation
{
    public class KeySwitchToJsonModel : IKeySwitchToText
    {
        public IText Translate( Domain.KeySwitches.KeySwitch source )
        {
            var id = 0;

            foreach( var x in source.Articulations )
            {
                var articulation = TranslateArticulation( x, id, 0, 0 );
                id++;
            }

            var result = new ArticulationMap();

            var serializeOption = new JsonSerializerOptions
            {
                Encoder       = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };

            var jsonText = JsonSerializer.Serialize( result, serializeOption );

            return new PlainText( jsonText );
        }

        private static CwArticulation TranslateArticulation(
            Articulation articulation,
            int id,
            int index,
            int groupId )
        {
            return new CwArticulation(
                id,
                articulation.ArticulationName.Value,
                index,
                groupId,
                "ffff0000",
                0,
                TranslateArticulationEvents( articulation ),
                TranslateArticulationTransform( articulation )
            );
        }

        private static IList<CwArticulation.Event> TranslateArticulationEvents( Articulation articulation )
        {
            CwArticulation.Event CreateEvent( IMidiMessage x ) =>
                new CwArticulation.Event(
                    x.Status.Value,
                    x.DataByte1.Value,
                    x.DataByte2.Value,
                    0x00,
                    0,
                    0,
                    0,
                    0
                );

            var result = new List<CwArticulation.Event>();

            result.AddRange( ( from x in articulation.MidiNoteOns select CreateEvent( x ) ).ToList() );
            result.AddRange( ( from x in articulation.MidiControlChanges select CreateEvent( x ) ).ToList() );
            result.AddRange( ( from x in articulation.MidiProgramChanges select CreateEvent( x ) ).ToList() );

            return result;
        }

        private static IList<CwArticulation.Transform> TranslateArticulationTransform( Articulation articulation )
        {
            return new List<CwArticulation.Transform>();
        }

    }
}