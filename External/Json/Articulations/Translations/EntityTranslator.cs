using System.Collections.Generic;
using System.IO;
using System.Text;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;
using ArticulationManager.Json.Articulations.Model;
using ArticulationManager.UseCases.Articulations.Exporting.Text;

using Newtonsoft.Json;

namespace ArticulationManager.Json.Articulations.Translations
{
    public class EntityTranslator : IEntityTranslator
    {
        public IEnumerable<IText> Translate( IEnumerable<Articulation> source )
        {
            var result = new List<IText>();

            foreach( var articulation in source )
            {
                var serializer = new JsonSerializer();

                var builder = new StringBuilder();
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
                result.Add( new IText.PlainText( builder.ToString() ) );
            }

            return result;
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