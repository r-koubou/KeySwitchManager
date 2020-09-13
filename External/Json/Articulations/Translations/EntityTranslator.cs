using System.Collections.Generic;
using System.IO;
using System.Text;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;
using ArticulationManager.Domain.Translations;
using ArticulationManager.Json.Articulations.Model;
using ArticulationManager.UseCases.Articulations.Exporting.Text;

using Newtonsoft.Json;

namespace ArticulationManager.Json.Articulations.Translations
{
    public class EntityTranslator : IArticulationToText
    {
        public IText Translate( IEnumerable<Articulation> source )
        {
            var builder = new StringBuilder();

            foreach( var articulation in source )
            {
                var serializer = new JsonSerializer();

                using var writer = new StringWriter( builder );

                List<MidiMessageModel> noteOn = new List<MidiMessageModel>();
                List<MidiMessageModel> controlChange = new List<MidiMessageModel>();
                List<MidiMessageModel> programChange = new List<MidiMessageModel>();

                ConvertMessageList( articulation.MidiNoteOns,        noteOn );
                ConvertMessageList( articulation.MidiControlChanges, controlChange );
                ConvertMessageList( articulation.MidiProgramChanges, programChange );

                var jsonObject = new ArticulationModel(
                    articulation.Id.Value,
                    EntityDateTimeService.ToDateTime( articulation.Created ),
                    EntityDateTimeService.ToDateTime( articulation.LastUpdated ),
                    articulation.DeveloperName.Value,
                    articulation.ProductName.Value,
                    articulation.ArticulationName.Value,
                    articulation.ArticulationType,
                    articulation.ArticulationGroup.Value,
                    articulation.ArticulationColor.Value,
                    new MidiModel(
                        noteOn,
                        controlChange,
                        programChange
                    )
                );

                serializer.Serialize( writer, jsonObject );
            }

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