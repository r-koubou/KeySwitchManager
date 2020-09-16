using System;
using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Value;

namespace KeySwitchManager.Domain.KeySwitches.Aggregate
{
    /// <summary>
    /// Represents the top element of the articulation structure.
    /// </summary>
    public class KeySwitch : IEquatable<KeySwitch>, IDuplicatable<KeySwitch>
    {
        public EntityGuid Id { get; }
        public Author Author { get; }
        public Description Description { get; }
        public EntityDateTime Created { get; }
        public EntityDateTime LastUpdated { get; }
        public DeveloperName DeveloperName { get; }
        public ProductName ProductName { get; }
        public InstrumentName InstrumentName { get; }
        public IEnumerable<Articulation> Articulations { get; }

        public KeySwitch(
            EntityGuid id,
            Author author,
            Description description,
            EntityDateTime created,
            EntityDateTime lastUpdated,
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName,
            IEnumerable<Articulation> articulations )
        {
            Id             = id;
            Author         = author;
            Description    = description;
            Created        = created;
            LastUpdated    = lastUpdated;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
            Articulations  = new List<Articulation>( articulations );
        }

        public KeySwitch Duplicate( KeySwitch source )
        {
            return new KeySwitch(
                new EntityGuid(),
                source.Author,
                source.Description,
                source.Created,
                source.LastUpdated,
                source.DeveloperName,
                source.ProductName,
                source.InstrumentName,
                source.Articulations
            );
        }

        #region Equals
        public bool Equals( KeySwitch? other )
        {
            if( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return other.Id.Equals( Id ) &&
                   other.Created.Equals( Created ) &&
                   other.LastUpdated.Equals( LastUpdated ) &&
                   other.DeveloperName.Equals( DeveloperName ) &&
                   other.ProductName.Equals( ProductName ) &&
                   other.InstrumentName.Equals( InstrumentName ) &&
                   other.Articulations.SequenceEqual( Articulations );
        }
        #endregion
    }
}