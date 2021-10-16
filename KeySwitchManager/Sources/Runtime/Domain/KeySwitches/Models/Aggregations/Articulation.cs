using System;
using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;

namespace KeySwitchManager.Domain.KeySwitches.Models.Aggregations
{
    public class Articulation : IEquatable<Articulation>
    {
        public ArticulationName ArticulationName { get; }
        public IDataList<IMidiChannelVoiceMessage> MidiNoteOns { get; }
        public IDataList<IMidiChannelVoiceMessage> MidiControlChanges { get; }
        public IDataList<IMidiChannelVoiceMessage> MidiProgramChanges { get; }
        public ExtraData ExtraData { get; }

        public Articulation(
            ArticulationName articulationName,
            IDataList<IMidiChannelVoiceMessage> midiNoteOns,
            IDataList<IMidiChannelVoiceMessage> midiControlChanges,
            IDataList<IMidiChannelVoiceMessage> midiProgramChanges,
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
                   MidiNoteOns.SequenceEqual( other.MidiNoteOns, IMidiMessage.EqualityComparer ) &&
                   MidiControlChanges.SequenceEqual( other.MidiControlChanges, IMidiMessage.EqualityComparer ) &&
                   MidiProgramChanges.SequenceEqual( other.MidiProgramChanges, IMidiMessage.EqualityComparer );
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

        #region IEqualityComparer
        public static readonly IEqualityComparer<Articulation> EqualityComparer = new DefaultComparer();
        private class DefaultComparer : IEqualityComparer<Articulation>
        {
            public bool Equals( Articulation? x, Articulation? y )
            {
                if( x == null && y == null )
                {
                    return true;
                }
                if( x != null )
                {
                    return x.Equals( y );
                }

                return y != null && y.Equals( x );
            }

            public int GetHashCode( Articulation obj )
                => obj.GetHashCode();
        }
        #endregion
    }
}