using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitches.Model;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class JsonModelToEntity : ITextToKeySwitch
    {
        public KeySwitch Translate( IText source )
        {
            var model = JsonConvert.DeserializeObject<KeySwitchModel>( source.Value );
            var articulations = TranslateImpl( model );

            return new IKeySwitchFactory.Default().Create(
                model.Id,
                model.Author,
                model.Description,
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