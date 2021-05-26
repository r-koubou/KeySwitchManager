using System.Collections.Generic;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models.Entities
{
    public class MidiModel
    {
        public IList<IMidiChannelVoiceMessageModel> NoteOn { get; set; } = new List<IMidiChannelVoiceMessageModel>();
        public IList<IMidiChannelVoiceMessageModel> ControlChange { get; set; } = new List<IMidiChannelVoiceMessageModel>();
        public IList<IMidiChannelVoiceMessageModel> ProgramChange { get; set; } = new List<IMidiChannelVoiceMessageModel>();

        public MidiModel()
        {}

        public MidiModel(
            IReadOnlyCollection<IMidiChannelVoiceMessageModel> noteOn,
            IReadOnlyCollection<IMidiChannelVoiceMessageModel> controlChange,
            IReadOnlyCollection<IMidiChannelVoiceMessageModel> programChange )
        {
            NoteOn        = new List<IMidiChannelVoiceMessageModel>( noteOn );
            ControlChange = new List<IMidiChannelVoiceMessageModel>( controlChange );
            ProgramChange = new List<IMidiChannelVoiceMessageModel>( programChange );
        }
    }
}