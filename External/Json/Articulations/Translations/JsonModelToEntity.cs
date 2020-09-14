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
    public class JsonModelToEntity : ITextToKeySwitch
    {
        public KeySwitch Translate( IText source )
        {
            var model = JsonConvert.DeserializeObject<KeySwitchModel>( source.Value );
            var articulations = TranslateImpl( model );

            return new IKeySwitchFactory.Default().Create(
                model.Id,
                model.Created,
                model.LastUpdated,
                model.DeveloperName,
                model.ProductName,
                model.InstrumentName,
                articulations
            );
        }

        private IEnumerable<Articulation> TranslateImpl( KeySwitchModel source )
        {
            var articulations = new List<Articulation>();

            foreach( var i in source.Articulations )
            {
                List<IMessage> noteOn = new List<IMessage>();
                List<IMessage> controlChange = new List<IMessage>();
                List<IMessage> programChange = new List<IMessage>();

                ConvertMessageList( i.MidiMessage.NoteOn,        noteOn,        new INoteOnFactory.Default() );
                ConvertMessageList( i.MidiMessage.ControlChange, controlChange, new IControlChangeFactory.Default() );
                ConvertMessageList( i.MidiMessage.ProgramChange, programChange, new IProgramChangeFactory.Default() );

                var articulation = new IArticulationFactory.Default().Create(
                    i.ArticulationName,
                    i.ArticulationType,
                    i.ArticulationGroup,
                    i.ArticulationColor,
                    noteOn,
                    controlChange,
                    programChange
                );

                articulations.Add( articulation );
            }

            return articulations;
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