using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Storage.Yaml.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Translators.Helpers
{
    internal static class YamlModelToKeySwitchHelper
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
                new Dictionary<string, string>( model.ExtraData )
            );
        }

        private static IReadOnlyCollection<Articulation> TranslateImpl( KeySwitchModel source )
        {
            var articulations = new List<Articulation>();

            foreach( var i in source.Articulations )
            {
                var noteOn = new List<IMidiChannelVoiceMessage>();
                var controlChange = new List<IMidiChannelVoiceMessage>();
                var programChange = new List<IMidiChannelVoiceMessage>();

                ConvertChannelVoiceMessage( i.MidiMessage.NoteOn,        noteOn,        IMidiNoteOnFactory.Default );
                ConvertChannelVoiceMessage( i.MidiMessage.ControlChange, controlChange, IMidiControlChangeFactory.Default );
                ConvertChannelVoiceMessage( i.MidiMessage.ProgramChange, programChange, IMidiProgramChangeFactory.Default );

                var articulation = IArticulationFactory.Default.Create(
                    i.Name,
                    noteOn,
                    controlChange,
                    programChange,
                    new Dictionary<string, string>( i.ExtraData )
                );

                articulations.Add( articulation );
            }

            return articulations;
        }

        #region Converting
        private static void ConvertChannelVoiceMessage(
            IEnumerable<IMidiChannelVoiceMessageModel> src,
            ICollection<IMidiChannelVoiceMessage> dest,
            IMidiChannelVoiceMessageFactory<IMidiChannelVoiceMessage> factory )
        {
            foreach( var i in src )
            {
                dest.Add(
                    factory.Create(
                        i.Channel,
                        i.Data1,
                        i.Data2
                    )
                );
            }
        }

        private static void ConvertMessageList(
            IEnumerable<IMidiMessageModel> src,
            ICollection<IMidiMessage> dest,
            IMidiMessageFactory<IMidiMessage> factory )
        {
            foreach( var i in src )
            {
                dest.Add(
                        factory.Create(
                        i.Status,
                        i.Data1,
                        i.Data2
                    )
                );
            }
        }
        #endregion
    }
}