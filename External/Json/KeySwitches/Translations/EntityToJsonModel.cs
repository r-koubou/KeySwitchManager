using System.Collections.Generic;
using System.IO;
using System.Text;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Services;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitches.Model;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Translations
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
                source.Author.Value,
                source.Description.Value,
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