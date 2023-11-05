using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches
{
    public abstract class OnMemoryKeySwitchRepository : IKeySwitchRepository
    {
        protected List<KeySwitch> KeySwitches { get; } = new();

        protected OnMemoryKeySwitchRepository() {}

        protected OnMemoryKeySwitchRepository( IReadOnlyCollection<KeySwitch> source )
        {
            KeySwitches.AddRange( source );
        }

        public virtual void Dispose()
        {
            KeySwitches.Clear();
        }

        public int Count()
            => KeySwitches.Count;

        public void WriteBinaryTo( Stream stream )
            => WriteBinaryToAsync( stream ).GetAwaiter().GetResult();

        public abstract Task WriteBinaryToAsync( Stream stream, CancellationToken cancellationToken = default );

        #region Save
        public async Task<IKeySwitchRepository.SaveResult> SaveAsync( KeySwitch keySwitch, CancellationToken cancellationToken = default )
        {
            var exist = KeySwitches.Find( x => x.Id.Value.Equals( keySwitch.Id.Value ) );

            if( exist != null )
            {
                var index = KeySwitches.IndexOf( exist );
                keySwitch.UpdateLastUpdatedDate( UtcDateTime.Now );
                KeySwitches[ index ] = keySwitch;
                return await Task.FromResult( new IKeySwitchRepository.SaveResult( 0, 1 ) );
            }

            KeySwitches.Add( keySwitch );

            return await Task.FromResult( new IKeySwitchRepository.SaveResult( 1, 0 ) );
        }

        public virtual async Task<int> FlushAsync( CancellationToken cancellationToken = default )
            => await Task.FromResult( Count() );
        #endregion

        #region Delete
        public async Task<int> DeleteAsync( KeySwitchId keySwitchId, CancellationToken cancellationToken = default )
        {
            var founds = await FindAsync( keySwitchId, cancellationToken );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName, CancellationToken cancellationToken = default )
        {
            var founds = await FindAsync( developerName, productName, instrumentName, cancellationToken );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( DeveloperName developerName, ProductName productName, CancellationToken cancellationToken = default )
        {
            var founds = await FindAsync( developerName, productName, cancellationToken );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( DeveloperName developerName, CancellationToken cancellationToken = default )
        {
            var founds = await FindAsync( developerName, cancellationToken );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( ProductName productName, CancellationToken cancellationToken = default )
        {
            var founds = await FindAsync( productName, cancellationToken );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAsync( InstrumentName instrumentName, CancellationToken cancellationToken = default )
        {
            var founds = await FindAsync( instrumentName, cancellationToken );
            return founds.Sum( x => KeySwitches.Remove( x ) ? 1 : 0 );
        }

        public async Task<int> DeleteAllAsync( CancellationToken cancellationToken = default )
        {
            var count = KeySwitches.Count;
            KeySwitches.Clear();
            return await Task.FromResult( count );
        }
        #endregion

        #region Find
        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( KeySwitchId keySwitchId, CancellationToken cancellationToken = default )
            => await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                x => x.Id.Value == keySwitchId.Value
            )));

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName, CancellationToken cancellationToken = default )
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

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName, CancellationToken cancellationToken = default )
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

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, CancellationToken cancellationToken = default )
        {
            var d = developerName.Value;

            return await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        d == DeveloperName.Any.Value || x.DeveloperName.Value.Contains( d )
            )));
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( ProductName productName, CancellationToken cancellationToken = default )
        {
            var p = productName.Value;

            return await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        p == ProductName.Any.Value || x.ProductName.Value.Contains( p )
            )));
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAsync( InstrumentName instrumentName, CancellationToken cancellationToken = default )
        {
            var i = instrumentName.Value;

            return await Task.FromResult( KeySwitchHelper.SortByAlphabetical(
                KeySwitches.FindAll(
                    x =>
                        i == InstrumentName.Any.Value || x.InstrumentName.Value.Contains( i )
            )));
        }

        public async Task<IReadOnlyCollection<KeySwitch>> FindAllAsync( CancellationToken cancellationToken = default )
            => await Task.FromResult( KeySwitchHelper.SortByAlphabetical( new List<KeySwitch>( KeySwitches ) ) );
        #endregion
    }
}
