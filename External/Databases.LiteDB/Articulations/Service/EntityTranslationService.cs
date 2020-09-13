using System.Collections.Generic;

using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.Databases.LiteDB.Articulations.Service
{
    public class EntityTranslationService : IDataTranslationService<Articulation, ArticulationModel>
    {
        public ArticulationModel Translate( Articulation source )
        {
            List<MidiMessageModel> noteOn = new List<MidiMessageModel>();
            List<MidiMessageModel> controlChange = new List<MidiMessageModel>();
            List<MidiMessageModel> programChange = new List<MidiMessageModel>();

            ConvertMessageList( source.MidiNoteOns, noteOn );
            ConvertMessageList( source.MidiControlChanges, controlChange );
            ConvertMessageList( source.MidiProgramChanges, programChange );

            return new ArticulationModel(
                EntityDateTimeService.ToDateTime( source.Created ),
                EntityDateTimeService.ToDateTime( source.LastUpdated ),
                source.DeveloperName.Value,
                source.ProductName.Value,
                source.ArticulationName.Value,
                source.ArticulationGroup.Value,
                source.ArticulationColor.Value,
                noteOn,
                controlChange,
                programChange
            );
        }

        private static void ConvertMessageList( IEnumerable<IMessage> src, List<MidiMessageModel> dest )
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
    }
}