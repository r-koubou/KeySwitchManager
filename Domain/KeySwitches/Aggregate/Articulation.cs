using System;
using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages.Aggregate;

namespace KeySwitchManager.Domain.KeySwitches.Aggregate
{
    public class Articulation : IEquatable<Articulation>
    {
        public ArticulationName ArticulationName { get; }
        public ArticulationType ArticulationType { get; }
        public ArticulationGroup ArticulationGroup { get; }
        public ArticulationColor ArticulationColor { get; }
        public IReadOnlyList<IMidiMessage> MidiNoteOns { get; }
        public IReadOnlyList<IMidiMessage> MidiControlChanges { get; }
        public IReadOnlyList<IMidiMessage> MidiProgramChanges { get; }

        public Articulation(
            ArticulationName articulationName,
            ArticulationType articulationType,
            ArticulationGroup articulationGroup,
            ArticulationColor articulationColor,
            IEnumerable<IMidiMessage> midiNoteOns,
            IEnumerable<IMidiMessage> midiControlChanges,
            IEnumerable<IMidiMessage> midiProgramChanges )
        {
            ArticulationName   = articulationName;
            ArticulationType   = articulationType;
            ArticulationGroup  = articulationGroup;
            ArticulationColor  = articulationColor;
            MidiNoteOns        = new List<IMidiMessage>( midiNoteOns );
            MidiControlChanges = new List<IMidiMessage>( midiControlChanges );
            MidiProgramChanges = new List<IMidiMessage>( midiProgramChanges );
        }

        #region Equals
        public bool Equals( Articulation? other )
        {
            if( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return ArticulationName.Equals( other.ArticulationName ) &&
                   ArticulationType == other.ArticulationType &&
                   ArticulationGroup.Equals( other.ArticulationGroup ) &&
                   ArticulationColor.Equals( other.ArticulationColor ) &&
                   MidiNoteOns.SequenceEqual( other.MidiNoteOns ) &&
                   MidiControlChanges.SequenceEqual( other.MidiControlChanges ) &&
                   MidiProgramChanges.SequenceEqual( other.MidiProgramChanges );
        }
        #endregion Equals
    }
}