using System;
using System.Collections.Generic;
using System.Linq;

using ArticulationManager.Domain.KeySwitches.Value;
using ArticulationManager.Domain.MidiMessages.Aggregate;

namespace ArticulationManager.Domain.KeySwitches.Aggregate
{
    public class Articulation : IEquatable<Articulation>
    {
        public ArticulationName ArticulationName { get; }
        public ArticulationType ArticulationType { get; }
        public ArticulationGroup ArticulationGroup { get; }
        public ArticulationColor ArticulationColor { get; }
        public IReadOnlyList<IMessage> MidiNoteOns { get; }
        public IReadOnlyList<IMessage> MidiControlChanges { get; }
        public IReadOnlyList<IMessage> MidiProgramChanges { get; }

        public Articulation(
            ArticulationName articulationName,
            ArticulationType articulationType,
            ArticulationGroup articulationGroup,
            ArticulationColor articulationColor,
            IEnumerable<IMessage> midiNoteOns,
            IEnumerable<IMessage> midiControlChanges,
            IEnumerable<IMessage> midiProgramChanges )
        {
            ArticulationName   = articulationName;
            ArticulationType   = articulationType;
            ArticulationGroup  = articulationGroup;
            ArticulationColor  = articulationColor;
            MidiNoteOns        = new List<IMessage>( midiNoteOns );
            MidiControlChanges = new List<IMessage>( midiControlChanges );
            MidiProgramChanges = new List<IMessage>( midiProgramChanges );
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