using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitches.Cakewalk.Models;

using Articulation = KeySwitchManager.Domain.KeySwitches.Entity.Articulation;
using CwArticulation = KeySwitchManager.Json.KeySwitches.Cakewalk.Models.Articulation;

namespace KeySwitchManager.Json.KeySwitches.Cakewalk.Translators
{
    public class KeySwitchToJsonModel : IKeySwitchToText
    {
        public IText Translate( KeySwitch source )
        {
            var id = 1;
            var index = 0;
            var groupId = 1;

            var articulations = new List<CwArticulation>();
            var groups = new List<Group>();

            foreach( var x in source.Articulations )
            {
                var a = TranslateArticulation( x, id, index, groupId );
                articulations.Add( a );

                var group = new Group( groupId, source.InstrumentName.Value );
                groups.Add( group );

                id++;
                index++;
                groupId++;
            }

            var articulationMap = new ArticulationMap(
                source.ProductName.Value,
                groups,
                articulations
            );

            var result = new CakewalkArticulationMap
            {
                ArticulationMaps = new List<ArticulationMap>{ articulationMap }
            };

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

        private static IReadOnlyList<MidiEvent> TranslateArticulationEvents( Articulation articulation )
        {
            MidiEvent CreateEvent( IMidiMessage x ) =>
                new MidiEvent{
                    Byte1 = x.Status.Value,
                    Byte2 = x.DataByte1.Value,
                    Byte3 = x.DataByte2.Value,
                };

            var result = new List<MidiEvent>();

            result.AddRange( ( from x in articulation.MidiNoteOns select CreateEvent( x ) ).ToList() );
            result.AddRange( ( from x in articulation.MidiControlChanges select CreateEvent( x ) ).ToList() );
            result.AddRange( ( from x in articulation.MidiProgramChanges select CreateEvent( x ) ).ToList() );

            return result;
        }

        private static IReadOnlyList<Transform> TranslateArticulationTransform( Articulation articulation )
        {
            return new List<Transform>();
        }

    }
}