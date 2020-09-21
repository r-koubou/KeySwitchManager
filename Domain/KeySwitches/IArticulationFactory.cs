using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Aggregate;

namespace KeySwitchManager.Domain.KeySwitches
{
    public interface IArticulationFactory
    {
        public Articulation Create(
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor );

        public Articulation Create(
            string articulationName,
            ArticulationType articulationType,
            int articulationGroup,
            int articulationColor,
            IEnumerable<IMidiMessage> midiNoteOns,
            IEnumerable<IMidiMessage> midiControlChanges,
            IEnumerable<IMidiMessage> midiProgramChanges );

        public static IArticulationFactory Default => new DefaultFactory();

        private class DefaultFactory : IArticulationFactory
        {
            public Articulation Create(
                string articulationName,
                ArticulationType articulationType,
                int articulationGroup,
                int articulationColor )
            {
                return new Articulation(
                    new ArticulationName( articulationName ),
                    articulationType,
                    new ArticulationGroup( articulationGroup ),
                    new ArticulationColor( articulationColor ),
                    new List<MidiNoteOn>(),
                    new List<MidiControlChange>(),
                    new List<MidiProgramChange>()
                );
            }

            public Articulation Create(
                string articulationName,
                ArticulationType articulationType,
                int articulationGroup,
                int articulationColor,
                IEnumerable<IMidiMessage> midiNoteOns,
                IEnumerable<IMidiMessage> midiControlChanges,
                IEnumerable<IMidiMessage> midiProgramChanges )
            {
                return new Articulation(
                    new ArticulationName( articulationName ),
                    articulationType,
                    new ArticulationGroup( articulationGroup ),
                    new ArticulationColor( articulationColor ),
                    midiNoteOns,
                    midiControlChanges,
                    midiProgramChanges
                );
            }
        }
    }
}