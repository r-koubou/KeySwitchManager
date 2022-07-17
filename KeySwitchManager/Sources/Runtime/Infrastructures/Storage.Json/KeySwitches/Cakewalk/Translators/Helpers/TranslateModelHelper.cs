using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk.Models;

using Articulation = KeySwitchManager.Domain.KeySwitches.Models.Aggregations.Articulation;
using CwArticulation = KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk.Models.Articulation;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk.Translators.Helpers
{
    internal static class TranslateModelHelper
    {
        public static CakewalkArticulationMap Translate( KeySwitch source )
        {
            var id = 1;
            var index = 0;
            var groupId = 1;

            var articulations = new List<CwArticulation>();

            foreach( var x in source.Articulations )
            {
                var a = TranslateArticulation( x, id, index, groupId );
                articulations.Add( a );

                id++;
                index++;
            }

            var articulationMap = new ArticulationMap(
                source.ProductName.Value,
                new[]{ new Group( 1, source.InstrumentName.Value ) },
                articulations
            );

            var result = new CakewalkArticulationMap
            {
                ArticulationMaps = new List<ArticulationMap>{ articulationMap }
            };

            return result;
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

        private static IReadOnlyCollection<MidiEvent> TranslateArticulationEvents( Articulation articulation )
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

        private static IReadOnlyCollection<Transform> TranslateArticulationTransform( Articulation articulation )
        {
            //TODO If support cakewalk dependent items
            return new List<Transform>();
        }

    }
}