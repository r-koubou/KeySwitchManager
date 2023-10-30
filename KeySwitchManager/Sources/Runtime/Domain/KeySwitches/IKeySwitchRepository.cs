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
            => SaveAsync( keySwitch, default).GetAwaiter().GetResult();

        public Task<SaveResult> SaveAsync( KeySwitch keySwitch, CancellationToken cancellationToken );
        #endregion

        #region Flush
        public int Flush()
            => FlushAsync( default ).GetAwaiter().GetResult();

        public Task<int> FlushAsync( CancellationToken cancellationToken )
            => Task.FromResult( 0 );
        #endregion

        #region Delete
        public int Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
            => DeleteAsync(developerName, productName, instrumentName, default ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName, CancellationToken cancellationToken );

        public int Delete( DeveloperName developerName, ProductName productName )
            => DeleteAsync( developerName, productName, default ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( DeveloperName developerName, ProductName productName, CancellationToken cancellationToken );

        public int Delete( DeveloperName developerName )
            => DeleteAsync( developerName, default ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( DeveloperName developerName, CancellationToken cancellationToken );

        public int Delete( ProductName productName )
            => DeleteAsync( productName, default ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( ProductName productName, CancellationToken cancellationToken );

        public int Delete( InstrumentName instrumentName )
            => DeleteAsync( instrumentName, default ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( InstrumentName instrumentName, CancellationToken cancellationToken );

        public int Delete( KeySwitchId keySwitchId )
            => DeleteAsync( keySwitchId, default ).GetAwaiter().GetResult();

        public Task<int> DeleteAsync( KeySwitchId keySwitchId, CancellationToken cancellationToken );

        public int DeleteAll()
            => DeleteAllAsync( default ).GetAwaiter().GetResult();

        public Task<int> DeleteAllAsync( CancellationToken cancellationToken );
        #endregion

        #region Find
        public IReadOnlyCollection<KeySwitch> Find( KeySwitchId keySwitchId )
            => FindAsync( keySwitchId, default ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( KeySwitchId keySwitchId, CancellationToken cancellationToken );

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName )
            => FindAsync( developerName, productName, instrumentName, default ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName, InstrumentName instrumentName, CancellationToken cancellationToken );

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName )
            => FindAsync( developerName, productName, default ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, ProductName productName, CancellationToken cancellationToken );

        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName )
            => FindAsync( developerName, default ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( DeveloperName developerName, CancellationToken cancellationToken );

        public IReadOnlyCollection<KeySwitch> Find( ProductName productName )
            => FindAsync( productName, default ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( ProductName productName, CancellationToken cancellationToken );

        public IReadOnlyCollection<KeySwitch> Find( InstrumentName instrumentName )
            => FindAsync( instrumentName, default ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAsync( InstrumentName instrumentName, CancellationToken cancellationToken );

        public IReadOnlyCollection<KeySwitch> FindAll()
            => FindAllAsync( default ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> FindAllAsync( CancellationToken cancellationToken );
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
