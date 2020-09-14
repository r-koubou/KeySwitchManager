using System.Collections.Generic;
using System.IO;
using System.Text;

using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.KeySwitches.Aggregate;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;
using ArticulationManager.Domain.Translations;
using ArticulationManager.Json.KeySwitches.Model;

using Newtonsoft.Json;

namespace ArticulationManager.Json.KeySwitches.Translations
{
    public class EntityToJsonModel : IKeySwitchToText
    {
        public IText Translate( KeySwitch source )
        {
            var builder = new StringBuilder();
            var serializer = new JsonSerializer();
            using var writer = new StringWriter( builder );

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
                    i.ArticulationType,
                    i.ArticulationGroup.Value,
                    i.ArticulationColor.Value,
                    new MidiModel(
                        noteOn,
                        controlChange,
                        programChange
                    )
                );

                articulationModels.Add( jsonObject );
            }

            var jsonRoot = new KeySwitchModel(
                source.Id.Value,
                EntityDateTimeService.ToDateTime( source.Created ),
                EntityDateTimeService.ToDateTime( source.LastUpdated ),
                source.DeveloperName.Value,
                source.ProductName.Value,
                source.InstrumentName.Value,
                articulationModels
            );

            serializer.Serialize( writer, jsonRoot );

            return new PlainText( builder.ToString() );
        }

        private static void ConvertMessageList(
            IEnumerable<IMessage> src,
            ICollection<MidiMessageModel> dest )
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