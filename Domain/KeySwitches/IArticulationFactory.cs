using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
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
            IEnumerable<IMidiMessage> midiProgramChanges,
            IReadOnlyDictionary<string, string> extraData );

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
                    new DataList<IMidiMessage>(),
                    new DataList<IMidiMessage>(),
                    new DataList<IMidiMessage>(),
                    ExtraData.Empty
                );
            }

            public Articulation Create(
                string articulationName,
                ArticulationType articulationType,
                int articulationGroup,
                int articulationColor,
                IEnumerable<IMidiMessage> midiNoteOns,
                IEnumerable<IMidiMessage> midiControlChanges,
                IEnumerable<IMidiMessage> midiProgramChanges,
                IReadOnlyDictionary<string, string> extraData )
            {
                return new Articulation(
                    new ArticulationName( articulationName ),
                    articulationType,
                    new ArticulationGroup( articulationGroup ),
                    new ArticulationColor( articulationColor ),
                    new DataList<IMidiMessage>( midiNoteOns ),
                    new DataList<IMidiMessage>( midiControlChanges ),
                    new DataList<IMidiMessage>( midiProgramChanges ),
                    IExtraDataFactory.Default.Create( extraData )
                );
            }
        }
    }
}