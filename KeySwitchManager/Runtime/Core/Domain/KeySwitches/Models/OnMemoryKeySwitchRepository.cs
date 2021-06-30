using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public class OnMemoryKeySwitchRepository : IKeySwitchRepository
    {
        protected List<KeySwitch> KeySwitches { get; }

        protected OnMemoryKeySwitchRepository()
        {
            KeySwitches = new List<KeySwitch>();
        }

        public OnMemoryKeySwitchRepository( IReadOnlyCollection<KeySwitch> source )
        {
            KeySwitches = new List<KeySwitch>( source );
        }

        public virtual void Dispose()
        {
            KeySwitches.Clear();
        }

        public int Count()
            => KeySwitches.Count;

        #region Save
        public IKeySwitchRepository.SaveResult Save( KeySwitch keySwitch )
        {
            var exist = KeySwitches.Find( x => x.Id.Value.Equals( keySwitch.Id.Value ) );

            if( exist != null )
            {
                var index = KeySwitches.IndexOf( exist );
                KeySwitches[ index ]  = keySwitch;
                return new IKeySwitchRepository.SaveResult( 0, 1 );
            }

            KeySwitches.Add( keySwitch );
            return new IKeySwitchRepository.SaveResult( 1, 0 );
        }

        public virtual int Flush()
            => Count();
        #endregion

        #region Delete
        public int Delete( DeveloperName developerName, ProductName productName )
        {
            var founds = Find( developerName, productName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            var founds = Find( developerName, productName, instrumentName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( KeySwitchId keySwitchId )
        {
            var founds = Find( keySwitchId );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public int DeleteAll()
        {
            var count = KeySwitches.Count;
            KeySwitches.Clear();
            return count;
        }
        #endregion

        #region Find
        public IReadOnlyCollection<KeySwitch> Find( KeySwitchId keySwitchId )
            =>  KeySwitches.FindAll(
                x => x.Id.Value == keySwitchId.Value
            );

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
            => KeySwitches.FindAll(
                x =>
                    x.DeveloperName == developerName &&
                    x.ProductName == productName &&
                    x.InstrumentName == instrumentName
            );

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName )
            => KeySwitches.FindAll(
                x =>
                    x.DeveloperName == developerName &&
                    x.ProductName == productName
            );

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName )
            => KeySwitches.FindAll(
                x =>
                    x.DeveloperName == developerName
            );

        public IReadOnlyCollection<KeySwitch> Find( ProductName productName )
            => KeySwitches.FindAll(
                x =>
                    x.ProductName == productName
            );

        public IReadOnlyCollection<KeySwitch> FindAll()
            => new List<KeySwitch>( KeySwitches );
        #endregion
    }
}