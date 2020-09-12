using System.Collections.Generic;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Domain.Articulations;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.MidiMessages;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.Databases.LiteDB.Articulations.Service
{
    public class ArticulationModelTranslationService : IDataTranslationService<ArticulationModel, Articulation>
    {
        public Articulation Translate( ArticulationModel source )
        {
            List<IMessage> noteOn = new List<IMessage>();
            List<IMessage> controlChange = new List<IMessage>();
            List<IMessage> programChange = new List<IMessage>();

            ConvertMessageList( source.NoteOn,        noteOn,        new INoteOnFactory.Default() );
            ConvertMessageList( source.ControlChange, controlChange, new IControlChangeFactory.Default() );
            ConvertMessageList( source.ProgramChange, programChange, new IProgramChangeFactory.Default() );

            return new IArticulationFactory.Default().Create(
                source.Id,
                source.Created,
                source.LastUpdated,
                source.DeveloperName,
                source.ProductName,
                source.ArticulationName,
                EnumHelper.Parse<ArticulationType>( source.ArticulationType ),
                source.ArticulationGroup,
                source.ArticulationColor,
                noteOn,
                controlChange,
                programChange
            );
        }

        private static void ConvertMessageList(
            IEnumerable<MidiMessageModel> src,
            ICollection<IMessage> dest,
            IMidiMessageFactory messageFactory )
        {
            foreach( var i in src )
            {
                dest.Add(
                    messageFactory.Create(
                        i.Status,
                        i.Channel,
                        i.DataByte1,
                        i.DataByte2
                    )
                );
            }
        }
    }
}