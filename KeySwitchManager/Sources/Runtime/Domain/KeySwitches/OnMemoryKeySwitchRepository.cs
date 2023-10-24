using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

using RkHelper.System;

namespace KeySwitchManager.Domain.KeySwitches
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
        public async Task<IKeySwitchRepository.SaveResult> SaveAsync( KeySwitch keySwitch )
        {
            var exist = KeySwitches.Find( x => x.Id.Value.Equals( keySwitch.Id.Value ) );

            if( exist != null )
            {
                var index = KeySwitches.IndexOf( exist );
                KeySwitches[ index ] = keySwitch;
                return await Task.FromResult( new IKeySwitchRepository.SaveResult( 0, 1 ) );
            }

            KeySwitches.Add( keySwitch );

            return await Task.FromResult( new IKeySwitchRepository.SaveResult( 1, 0 ) );
        }

        public virtual async Task<int> FlushAsync()
            => await Task.FromResult( Count() );
        #endregion

        #region Delete
        public async Task<int> DeleteAsync( KeySwitchId keySwitchId )
        {
            var founds = await FindAsync( keySwitchId );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            var founds = await FindAsync( developerName, productName, instrumentName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( DeveloperName developerName, ProductName productName )
        {
            var founds = await FindAsync( developerName, productName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( DeveloperName developerName )
        {
            var founds = await FindAsync( developerName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( ProductName productName )
        {
            var founds = await FindAsync( productName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( InstrumentName instrumentName )
        {
            var founds = await FindAsync( instrumentName );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAllAsync()
        {
            var count = KeySwitches.Count;
            KeySwitches.Clear();
            return await Task.FromResult( count );
        }
        #endregion

        #region Find
        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( KeySwitchId keySwitchId )
            => await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                x => x.Id.Value == keySwitchId.Value
            )));

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
        {
            var d = developerName.Value;
            var p = productName.Value;
            var i = instrumentName.Value;

            return await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        ( d == DeveloperName.Any.Value || x.DeveloperName.Value.Contains( d ) ) &&
                        ( p == ProductName.Any.Value || x.ProductName.Value.Contains( p ) ) &&
                        ( i == InstrumentName.Any.Value || x.InstrumentName.Value.Contains( i ) )
                )));
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName )
        {
            var d = developerName.Value;
            var p = productName.Value;

            return await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        ( d == DeveloperName.Any.Value || x.DeveloperName.Value.Contains( d ) ) &&
                        ( p == ProductName.Any.Value || x.ProductName.Value.Contains( p ) )
                )));
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName )
        {
            var d = developerName.Value;

            return await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        d == DeveloperName.Any.Value || x.DeveloperName.Value.Contains( d )
            )));
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( ProductName productName )
        {
            var p = productName.Value;

            return await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        p == ProductName.Any.Value || x.ProductName.Value.Contains( p )
            )));
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( InstrumentName instrumentName )
        {
            var i = instrumentName.Value;

            return await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        i == InstrumentName.Any.Value || x.InstrumentName.Value.Contains( i )
            )));
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAllAsync()
            => await Task.FromResult( KeySwitchHelper.SortByAlphabetical( new List<KeySwitch>( KeySwitches ) ) );
        #endregion
    }
}
