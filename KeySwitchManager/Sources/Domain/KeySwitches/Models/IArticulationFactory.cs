using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IArticulationFactory
    {
        public Articulation Create(
            string articulationName,
            int articulationGroup,
            int articulationColor );

        public Articulation Create(
            string articulationName,
            IEnumerable<IMidiChannelVoiceMessage> midiNoteOns,
            IEnumerable<IMidiChannelVoiceMessage> midiControlChanges,
            IEnumerable<IMidiChannelVoiceMessage> midiProgramChanges,
            IReadOnlyDictionary<string, string> extraData );

        public static IArticulationFactory Default => new DefaultFactory();

        private class DefaultFactory : IArticulationFactory
        {
            public Articulation Create(
                string articulationName,
                int articulationGroup,
                int articulationColor )
            {
                return new Articulation(
                    new ArticulationName( articulationName ),
                    new DataList<IMidiChannelVoiceMessage>(),
                    new DataList<IMidiChannelVoiceMessage>(),
                    new DataList<IMidiChannelVoiceMessage>(),
                    new ExtraData()
                );
            }

            public Articulation Create(
                string articulationName,
                IEnumerable<IMidiChannelVoiceMessage> midiNoteOns,
                IEnumerable<IMidiChannelVoiceMessage> midiControlChanges,
                IEnumerable<IMidiChannelVoiceMessage> midiProgramChanges,
                IReadOnlyDictionary<string, string> extraData )
            {
                return new Articulation(
                    new ArticulationName( articulationName ),
                    new DataList<IMidiChannelVoiceMessage>( midiNoteOns ),
                    new DataList<IMidiChannelVoiceMessage>( midiControlChanges ),
                    new DataList<IMidiChannelVoiceMessage>( midiProgramChanges ),
                    IExtraDataFactory.Default.Create( extraData )
                );
            }
        }
    }
}