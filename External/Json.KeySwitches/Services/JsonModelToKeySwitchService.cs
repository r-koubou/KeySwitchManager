using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Json.KeySwitches.Models;

namespace KeySwitchManager.Json.KeySwitches.Services
{
    internal static class JsonModelToKeySwitchService
    {
        public static KeySwitch Translate( KeySwitchModel model )
        {
            var articulations = TranslateImpl( model );

            return IKeySwitchFactory.Default.Create(
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

        private static IEnumerable<Articulation> TranslateImpl( KeySwitchModel source )
        {
            var articulations = new List<Articulation>();

            foreach( var i in source.Articulations )
            {
                List<IMessage> noteOn = new List<IMessage>();
                List<IMessage> controlChange = new List<IMessage>();
                List<IMessage> programChange = new List<IMessage>();

                ConvertMessageList( i.MidiMessage.NoteOn,        noteOn,        INoteOnFactory.Default );
                ConvertMessageList( i.MidiMessage.ControlChange, controlChange, IControlChangeFactory.Default );
                ConvertMessageList( i.MidiMessage.ProgramChange, programChange, IProgramChangeFactory.Default );

                var articulation = IArticulationFactory.Default.Create(
                    i.Name,
                    i.Type,
                    i.Group,
                    i.Color,
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
                        i.Data1,
                        i.Data2
                    )
                );
            }
        }

    }
}