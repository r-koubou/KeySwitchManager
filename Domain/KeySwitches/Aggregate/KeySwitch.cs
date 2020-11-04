using System;
using System.Linq;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Value;

namespace KeySwitchManager.Domain.KeySwitches.Aggregate
{
    /// <summary>
    /// Represents the top element of the articulation structure.
    /// </summary>
    public class KeySwitch : IEquatable<KeySwitch>
    {
        public EntityGuid Id { get; }
        public Author Author { get; }
        public Description Description { get; }
        public EntityDateTime Created { get; }
        public EntityDateTime LastUpdated { get; }
        public DeveloperName DeveloperName { get; }
        public ProductName ProductName { get; }
        public InstrumentName InstrumentName { get; }
        public IDataList<Articulation> Articulations { get; }
        public ExtraData ExtraData { get; }

        public KeySwitch(
            EntityGuid id,
            Author author,
            Description description,
            EntityDateTime created,
            EntityDateTime lastUpdated,
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName,
            IDataList<Articulation> articulations,
            ExtraData extraData )
        {
            Id             = id;
            Author         = author;
            Description    = description;
            Created        = created;
            LastUpdated    = lastUpdated;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
            Articulations  = articulations;
            ExtraData      = extraData;
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