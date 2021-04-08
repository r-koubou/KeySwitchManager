using System;
using System.Linq;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Values;
using KeySwitchManager.Domain.MidiMessages.Entity;

namespace KeySwitchManager.Domain.KeySwitches.Entity
{
    public class Articulation : IEquatable<Articulation>
    {
        public ArticulationName ArticulationName { get; }
        public IDataList<IMidiMessage> MidiNoteOns { get; }
        public IDataList<IMidiMessage> MidiControlChanges { get; }
        public IDataList<IMidiMessage> MidiProgramChanges { get; }
        public ExtraData ExtraData { get; }

        public Articulation(
            ArticulationName articulationName,
            IDataList<IMidiMessage> midiNoteOns,
            IDataList<IMidiMessage> midiControlChanges,
            IDataList<IMidiMessage> midiProgramChanges,
            ExtraData extraData )
        {
            ArticulationName   = articulationName;
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
                   MidiNoteOns.SequenceEqual( other.MidiNoteOns ) &&
                   MidiControlChanges.SequenceEqual( other.MidiControlChanges ) &&
                   MidiProgramChanges.SequenceEqual( other.MidiProgramChanges );
        }

        public override bool Equals( object? obj )
        {
            return obj != null && Equals( obj as Articulation );
        }

        public override int GetHashCode() =>
            HashCode.Combine( ArticulationName,
                              MidiNoteOns,
                              MidiControlChanges,
                              MidiProgramChanges,
                              ExtraData );
        #endregion Equals
    }
}