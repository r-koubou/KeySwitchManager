using System.Collections.Generic;

using Databases.LiteDB.KeySwitches.KeySwitches.Models;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Services;
using KeySwitchManager.Domain.Translations;

using LiteDB;

namespace Databases.LiteDB.KeySwitches.KeySwitches.Translations
{
    public class EntityToDbModel : IDataTranslator<KeySwitch, KeySwitchModel>
    {
        public KeySwitchModel Translate( KeySwitch source )
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
                    i.ArticulationGroup.Value,
                    i.ArticulationColor.Value,
                    noteOn,
                    controlChange,
                    programChange
                );

                articulationModels.Add( articulation );
            }

            var extra = new Dictionary<string, BsonValue>();
            foreach( var key in source.ExtraData.Keys )
            {
                var k = key.Value;
                var v = source.ExtraData[ key ];
                extra.Add( k, new BsonValue( v.Value ) );
            }

            return new KeySwitchModel(
                source.Id.Value,
                source.Author.Value,
                source.Description.Value,
                EntityDateTimeService.ToDateTime( source.Created ),
                EntityDateTimeService.ToDateTime( source.LastUpdated ),
                source.DeveloperName.Value,
                source.ProductName.Value,
                source.InstrumentName.Value,
                articulationModels,
                extra
            );
        }

        private static void ConvertMessageList( IReadOnlyCollection<IMidiMessage> src, ICollection<MidiMessageModel> dest )
        {
            foreach( var i in src )
            {
                dest.Add(
                    new MidiMessageModel(
                        i.Status.Value,
                        i.Channel.Value,
                        i.DataByte1.Value,
                        i.DataByte2.Value
                    )
                );
            }
        }
    }
}