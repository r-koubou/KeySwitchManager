using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

using RkHelper.Number;

namespace KeySwitchManager.Domain.KeySwitches
{
    public interface IKeySwitchRepository : IDisposable
    {
        public IObservable<string> OnLogging { get; }

        public int Count();

        #region Save
        public SaveResult Save( KeySwitch keySwitch )
            => SaveAsync( keySwitch ).GetAwaiter().GetResult();

        public Task<SaveResult> SaveAsync( KeySwitch keySwitch, CancellationToken cancellationToken = default );
        #endregion

        #region Flush
        public int Flush()
            => FlushAsync().GetAwaiter().GetResult();

        public Task<int> FlushAsync( CancellationToken cancellationToken = default )
            => Task.FromResult( 0 );
        #endregion

        #region Delete
        public int Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
            => DeleteAsync(developerName, productName, instrumentName ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName, CancellationToken cancellationToken = default );

        public int Delete( DeveloperName developerName, ProductName productName )
            => DeleteAsync( developerName, productName ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( DeveloperName developerName, ProductName productName, CancellationToken cancellationToken = default );

        public int Delete( DeveloperName developerName )
            => DeleteAsync( developerName ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( DeveloperName developerName, CancellationToken cancellationToken = default );

        public int Delete( ProductName productName )
            => DeleteAsync( productName ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( ProductName productName, CancellationToken cancellationToken = default );

        public int Delete( InstrumentName instrumentName )
            => DeleteAsync( instrumentName ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( InstrumentName instrumentName, CancellationToken cancellationToken = default );

        public int Delete( KeySwitchId keySwitchId )
            => DeleteAsync( keySwitchId ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( KeySwitchId keySwitchId, CancellationToken cancellationToken = default );

        public int DeleteAll()
            => DeleteAllAsync().GetAwaiter().GetResult();

        public Task<int> DeleteAllAsync( CancellationToken cancellationToken = default );
        #endregion

        #region Find
        public IReadOnlyCollection<KeySwitch> Find( KeySwitchId keySwitchId )
            => FindAsync( keySwitchId ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( KeySwitchId keySwitchId, CancellationToken cancellationToken = default );

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
            => FindAsync( developerName, productName, instrumentName ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName, CancellationToken cancellationToken = default );

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName )
            => FindAsync( developerName, productName ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName, CancellationToken cancellationToken = default );

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName )
            => FindAsync( developerName ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, CancellationToken cancellationToken = default );

        public IReadOnlyCollection<KeySwitch> Find( ProductName productName )
            => FindAsync( productName ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( ProductName productName, CancellationToken cancellationToken = default );

        public IReadOnlyCollection<KeySwitch> Find( InstrumentName instrumentName )
            => FindAsync( instrumentName ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( InstrumentName instrumentName, CancellationToken cancellationToken = default );

        public IReadOnlyCollection<KeySwitch> FindAll()
            => FindAllAsync().GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAllAsync( CancellationToken cancellationToken = default );
        #endregion

        public class SaveResult : IEquatable<SaveResult>
        {
            private const int MinValue = 0;
            private const int MaxValue = int.MaxValue - 1;

            public int Inserted { get; }
            public int Updated { get; }

            public bool Any =>
                Inserted > MinValue &&
                Updated > MinValue;

            public SaveResult( int inserted, int updated )
            {
                NumberHelper.ValidateRange( inserted, MinValue, MaxValue );
                NumberHelper.ValidateRange( updated,  MinValue, MaxValue );
                Inserted = inserted;
                Updated  = updated;
            }

            public bool Equals( SaveResult? other )
            {
                return other != null &&
                       other.Inserted == Inserted &&
                       other.Updated == Updated;
            }
        }
    }
}
