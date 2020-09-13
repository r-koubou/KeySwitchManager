using System.Collections.Generic;

using ArticulationManager.Domain.Articulations;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.Translations;
using ArticulationManager.Json.Articulations.Model;

using Newtonsoft.Json;

namespace ArticulationManager.Json.Articulations.Translations
{
    public class EntityModelTranslator : ITextToArticulation
    {
        public IEnumerable<Articulation> Translate( IText source )
        {
            var result = new List<Articulation>();
            var model = JsonConvert.DeserializeObject<ArticulationModel>( source.Value );#
#error TODO エンティティの構造修正に合わせて変換の修正必須
            result.Add( TranslateImpl( model ) );

            return result;
        }

        private Articulation TranslateImpl( ArticulationModel source )
        {
            List<IMessage> noteOn = new List<IMessage>();
            List<IMessage> controlChange = new List<IMessage>();
            List<IMessage> programChange = new List<IMessage>();

            ConvertMessageList( source.MidiMessage.NoteOn,        noteOn,        new INoteOnFactory.Default() );
            ConvertMessageList( source.MidiMessage.ControlChange, controlChange, new IControlChangeFactory.Default() );
            ConvertMessageList( source.MidiMessage.ProgramChange, programChange, new IProgramChangeFactory.Default() );

            return new IArticulationFactory.Default().Create(
                source.Id,
                source.Created,
                source.LastUpdated,
                source.DeveloperName,
                source.ProductName,
                source.ArticulationName,
                source.ArticulationType,
                source.ArticulationGroup,
                source.ArticulationColor,
                noteOn,
                controlChange,
                programChange
            );
        }

        private static void ConvertMessageList(
            IEnumerable<MidiMessageModel> src,
            List<IMessage> dest,
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