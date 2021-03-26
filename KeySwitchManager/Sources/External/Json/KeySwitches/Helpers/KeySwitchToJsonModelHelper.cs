using System.Collections.Generic;

using KeySwitchManager.Domain.Commons.Helper;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Json.KeySwitches.Models;

namespace KeySwitchManager.Json.KeySwitches.Helpers
{
    internal static class KeySwitchToJsonModelHelper
    {
        public static KeySwitchModel Translate( KeySwitch source )
        {
            var articulationModels = new List<ArticulationModel>();

            foreach( var i in source.Articulations )
            {
                var noteOn = new List<MidiMessageModel>();
                var controlChange = new List<MidiMessageModel>();
                var programChange = new List<MidiMessageModel>();

                ConvertMessageList( i.MidiNoteOns,        noteOn );
                ConvertMessageList( i.MidiControlChanges, controlChange );
                ConvertMessageList( i.MidiProgramChanges, programChange );

                var jsonObject = new ArticulationModel(
                    i.ArticulationName.Value,
                    new MidiModel(
                        noteOn,
                        controlChange,
                        programChange
                    ),
                    ConvertExtraData( i.ExtraData )
                );

                articulationModels.Add( jsonObject );
            }

            return new KeySwitchModel(
                source.Id.Value,
                source.Author.Value,
                source.Description.Value,
                UtcDateTimeHelper.ToDateTime( source.Created ),
                UtcDateTimeHelper.ToDateTime( source.LastUpdated ),
                source.DeveloperName.Value,
                source.ProductName.Value,
                source.InstrumentName.Value,
                articulationModels,
                IExtraDataFactory.Default.Create( source.ExtraData )
            );
        }

        private static void ConvertMessageList(
            IEnumerable<IMidiMessage> src,
            ICollection<MidiMessageModel> dest )
        {
            foreach( var i in src )
            {
                dest.Add(
                    new MidiMessageModel(
                        i.Status.Value,
                        i.DataByte1.Value,
                        i.DataByte2.Value
                    )
                );
            }
        }

        private static Dictionary<string, string> ConvertExtraData( ExtraData source )
        {
            var extra = new Dictionary<string, string>();

            foreach( var key in source.Keys )
            {
                var k = key.Value;
                var v = source[ key ].Value;
                extra.Add( key.Value, v );
            }

            return extra;
        }

    }
}