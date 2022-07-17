using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;

using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

using RkHelper.System;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public class OnMemoryKeySwitchRepository : IKeySwitchRepository
    {
        protected List<KeySwitch> KeySwitches { get; }

        private readonly Subject<string> logging = new();
        public IObservable<string> OnLogging => logging;

        public OnMemoryKeySwitchRepository()
        {
            KeySwitches = new List<KeySwitch>();
        }

        public OnMemoryKeySwitchRepository( IReadOnlyCollection<KeySwitch> source )
        {
            KeySwitches = new List<KeySwitch>( source );
        }

        public virtual void Dispose()
        {
            Disposer.Dispose( logging );
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
        public int Delete( KeySwitchId keySwitchId )
        {
            var founds = Find( keySwitchId );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            var founds = Find( developerName, productName, instrumentName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( DeveloperName developerName, ProductName productName )
        {
            var founds = Find( developerName, productName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( DeveloperName developerName )
        {
            var founds = Find( developerName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( ProductName productName )
        {
            var founds = Find( productName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public int Delete( InstrumentName instrumentName )
        {
            var founds = Find( instrumentName );
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
            => KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                x => x.Id.Value == keySwitchId.Value
            ));

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            var d = developerName.Value;
            var p = productName.Value;
            var i = instrumentName.Value;

            return KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        ( d == DeveloperName.Any.Value || x.DeveloperName.Value.Contains( d ) ) &&
                        ( p == ProductName.Any.Value || x.ProductName.Value.Contains( p ) ) &&
                        ( i == InstrumentName.Any.Value || x.InstrumentName.Value.Contains( i ) )
                ));
        }

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName )
        {
            var d = developerName.Value;
            var p = productName.Value;

            return KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        ( d == DeveloperName.Any.Value || x.DeveloperName.Value.Contains( d ) ) &&
                        ( p == ProductName.Any.Value || x.ProductName.Value.Contains( p ) )
                ));
        }

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName )
        {
            var d = developerName.Value;

            return KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        d == DeveloperName.Any.Value || x.DeveloperName.Value.Contains( d )
            ));
        }

        public IReadOnlyCollection<KeySwitch> Find( ProductName productName )
        {
            var p = productName.Value;

            return KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        p == ProductName.Any.Value || x.ProductName.Value.Contains( p )
            ));
        }

        public IReadOnlyCollection<KeySwitch> Find( InstrumentName instrumentName )
        {
            var i = instrumentName.Value;

            return KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        i == InstrumentName.Any.Value || x.InstrumentName.Value.Contains( i )
            ));
        }

        public IReadOnlyCollection<KeySwitch> FindAll()
            => KeySwitchHelper.SortByAlphabetical( new List<KeySwitch>( KeySwitches ) );
        #endregion
    }
}