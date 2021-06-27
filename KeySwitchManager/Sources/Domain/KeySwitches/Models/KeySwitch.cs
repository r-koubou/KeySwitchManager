using System;
using System.Linq;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    /// <summary>
    /// Represents the top element of the articulation structure.
    /// </summary>
    public class KeySwitch : IEquatable<KeySwitch>
    {
        public KeySwitchId Id { get; }
        public Author Author { get; }
        public Description Description { get; }
        public UtcDateTime Created { get; }
        public UtcDateTime LastUpdated { get; }
        public DeveloperName DeveloperName { get; }
        public ProductName ProductName { get; }
        public InstrumentName InstrumentName { get; }
        public IDataList<Articulation> Articulations { get; }
        public ExtraData ExtraData { get; }

        public KeySwitch(
            KeySwitchId id,
            Author author,
            Description description,
            UtcDateTime created,
            UtcDateTime lastUpdated,
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
                   other.Articulations.SequenceEqual( Articulations, Articulation.EqualityComparer );
        }

        public override bool Equals( object? obj )
        {
            return obj != null && Equals( obj as KeySwitch );
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add( Id );
            hashCode.Add( Author );
            hashCode.Add( Description );
            hashCode.Add( Created );
            hashCode.Add( LastUpdated );
            hashCode.Add( DeveloperName );
            hashCode.Add( ProductName );
            hashCode.Add( InstrumentName );
            hashCode.Add( Articulations );
            hashCode.Add( ExtraData );

            return hashCode.ToHashCode();
        }
        #endregion
    }
}