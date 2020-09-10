using System;
using System.Collections.Generic;
using System.Linq;

using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;

namespace ArticulationManager.Domain.Articulations.Aggregate
{
    public class Articulation : IEquatable<Articulation>
    {
        public EntityGuid Id { get; }
        public EntityDateTime Created { get; }
        public EntityDateTime LastUpdated { get; }
        public DeveloperName DeveloperName { get; }
        public ProductName ProductName { get; }
        public ArticulationName ArticulationName { get; }
        public ArticulationType ArticulationType { get; }
        public ArticulationGroup ArticulationGroup { get; }
        public ArticulationColor ArticulationColor { get; }
        public IReadOnlyList<IMessage> MidiNoteOns { get; }
        public IReadOnlyList<IMessage> MidiControlChanges { get; }
        public IReadOnlyList<IMessage> MidiProgramChanges { get; }

        public Articulation(
            EntityGuid id,
            EntityDateTime created,
            EntityDateTime lastUpdated,
            DeveloperName developerName,
            ProductName productName,
            ArticulationName articulationName,
            ArticulationType articulationType,
            ArticulationGroup articulationGroup,
            ArticulationColor articulationColor,
            IEnumerable<IMessage> midiNoteOns,
            IEnumerable<IMessage> midiControlChanges,
            IEnumerable<IMessage> midiProgramChanges )
        {
            Id                 = id;
            Created            = created;
            LastUpdated        = lastUpdated;
            DeveloperName      = developerName;
            ProductName        = productName;
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

            return Id.Equals( other.Id ) &&
                   Created.Equals( other.Created ) &&
                   LastUpdated.Equals( other.LastUpdated ) &&
                   DeveloperName.Equals( other.DeveloperName ) &&
                   ProductName.Equals( other.ProductName ) &&
                   ArticulationName.Equals( other.ArticulationName ) &&
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