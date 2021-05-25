using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Midi.Models;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Entities;
using KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Models;

namespace KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Translators.Helpers
{
    internal static class JsonModelToKeySwitchHelper
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
                articulations,
                model.ExtraData
            );
        }

        private static IReadOnlyCollection<Articulation> TranslateImpl( KeySwitchModel source )
        {
            var articulations = new List<Articulation>();

            foreach( var i in source.Articulations )
            {
                var noteOn = new List<IMidiChannelVoiceMessage>();
                List<IMidiMessage> controlChange = new List<IMidiMessage>();
                List<IMidiMessage> programChange = new List<IMidiMessage>();

                ConvertMessageList( i.MidiMessage.NoteOn,        noteOn,        IMidiNoteOnFactory.Default );
                ConvertMessageList( i.MidiMessage.ControlChange, controlChange, IMidiControlChangeFactory.Default );
                ConvertMessageList( i.MidiMessage.ProgramChange, programChange, IMidiProgramChangeFactory.Default );

                var articulation = IArticulationFactory.Default.Create(
                    i.Name,
                    noteOn,
                    controlChange,
                    programChange,
                    i.ExtraData
                );

                articulations.Add( articulation );
            }

            return articulations;
        }

        private static void ConvertMessageList(
            IEnumerable<MidiMessageModel> src,
            ICollection<IMidiChannelVoiceMessage> dest,
            IMidiChannelVoiceMessageFactory<IMidiChannelVoiceMessage> messageFactory )
        {
            foreach( var i in src )
            {
                dest.Add(
                    messageFactory.Create(
                        i.Channel,
                        i.Data1,
                        i.Data2
                    )
                );
            }
        }
    }
}