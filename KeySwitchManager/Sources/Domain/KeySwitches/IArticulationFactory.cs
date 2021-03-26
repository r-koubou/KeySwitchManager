using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Entity;
using KeySwitchManager.Domain.KeySwitches.Values;
using KeySwitchManager.Domain.MidiMessages.Entity;

namespace KeySwitchManager.Domain.KeySwitches
{
    public interface IArticulationFactory
    {
        public Articulation Create(
            string articulationName,
            int articulationGroup,
            int articulationColor );

        public Articulation Create(
            string articulationName,
            IEnumerable<IMidiMessage> midiNoteOns,
            IEnumerable<IMidiMessage> midiControlChanges,
            IEnumerable<IMidiMessage> midiProgramChanges,
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
                    new DataList<IMidiMessage>(),
                    new DataList<IMidiMessage>(),
                    new DataList<IMidiMessage>(),
                    new ExtraData()
                );
            }

            public Articulation Create(
                string articulationName,
                IEnumerable<IMidiMessage> midiNoteOns,
                IEnumerable<IMidiMessage> midiControlChanges,
                IEnumerable<IMidiMessage> midiProgramChanges,
                IReadOnlyDictionary<string, string> extraData )
            {
                return new Articulation(
                    new ArticulationName( articulationName ),
                    new DataList<IMidiMessage>( midiNoteOns ),
                    new DataList<IMidiMessage>( midiControlChanges ),
                    new DataList<IMidiMessage>( midiProgramChanges ),
                    IExtraDataFactory.Default.Create( extraData )
                );
            }
        }
    }
}