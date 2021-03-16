using System.Collections.Generic;

using Database.LiteDB.KeySwitch.KeySwitch.Models;

using KeySwitchManager.Domain.Commons.Helper;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Domain.Translations;

namespace Database.LiteDB.KeySwitch.KeySwitch.Translations
{
    public class EntityToDbModel : IDataTranslator<KeySwitchManager.Domain.KeySwitches.KeySwitch, KeySwitchModel>
    {
        public KeySwitchModel Translate( KeySwitchManager.Domain.KeySwitches.KeySwitch source )
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

                var articulation = new ArticulationModel(
                    i.ArticulationName.Value,
                    noteOn,
                    controlChange,
                    programChange,
                    ConvertExtraData( i.ExtraData )
                );

                articulationModels.Add( articulation );
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
                ConvertExtraData( source.ExtraData )
            );
        }

        private static void ConvertMessageList( IReadOnlyCollection<IMidiMessage> src, ICollection<MidiMessageModel> dest )
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

        private static Dictionary<string, object> ConvertExtraData( ExtraData source )
        {
            var extra = new Dictionary<string, object>();

            foreach( var keyValuePair in source )
            {
                var k = keyValuePair.Key.Value;
                var v = keyValuePair.Value.Value;
                extra[ k ] = v;
            }

            return extra;
        }

    }
}