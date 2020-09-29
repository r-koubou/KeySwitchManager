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
        public IReadOnlyCollection<IMidiMessage> MidiNoteOns { get; }
        public IReadOnlyCollection<IMidiMessage> MidiControlChanges { get; }
        public IReadOnlyCollection<IMidiMessage> MidiProgramChanges { get; }
        public ExtraData ExtraData { get; }

        public Articulation(
            ArticulationName articulationName,
            ArticulationType articulationType,
            ArticulationGroup articulationGroup,
            ArticulationColor articulationColor,
            IReadOnlyCollection<IMidiMessage> midiNoteOns,
            IReadOnlyCollection<IMidiMessage> midiControlChanges,
            IReadOnlyCollection<IMidiMessage> midiProgramChanges,
            ExtraData extraData )
        {
            ArticulationName   = articulationName;
            ArticulationType   = articulationType;
            ArticulationGroup  = articulationGroup;
            ArticulationColor  = articulationColor;
            MidiNoteOns        = midiNoteOns;
            MidiControlChanges = midiControlChanges;
            MidiProgramChanges = midiProgramChanges;
            ExtraData          = extraData;
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