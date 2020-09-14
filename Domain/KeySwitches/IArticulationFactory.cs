using System.Collections.Generic;

using ArticulationManager.Domain.KeySwitches.Aggregate;
using ArticulationManager.Domain.KeySwitches.Value;
using ArticulationManager.Domain.MidiMessages.Aggregate;

namespace ArticulationManager.Domain.KeySwitches
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
            IEnumerable<IMessage> midiNoteOns,
            IEnumerable<IMessage> midiControlChanges,
            IEnumerable<IMessage> midiProgramChanges );

        public class Default : IArticulationFactory
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
                    new List<NoteOn>(),
                    new List<ControlChange>(),
                    new List<ProgramChange>()
                );
            }

            public Articulation Create(
                string articulationName,
                ArticulationType articulationType,
                int articulationGroup,
                int articulationColor,
                IEnumerable<IMessage> midiNoteOns,
                IEnumerable<IMessage> midiControlChanges,
                IEnumerable<IMessage> midiProgramChanges )
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