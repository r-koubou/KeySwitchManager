using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models.Values;

using RkHelper.Number;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchRepository : IDisposable
    {
        public IObservable<string> OnLogging { get; }

        public int Count();
        public SaveResult Save( KeySwitch keySwitch );
        public int Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public int Delete( DeveloperName developerName, ProductName productName );
        public int Delete( DeveloperName developerName );
        public int Delete( ProductName productName );
        public int Delete( InstrumentName instrumentName );
        public int Delete( KeySwitchId keySwitchId );
        public int DeleteAll();
        public IReadOnlyCollection<KeySwitch> Find( KeySwitchId keySwitchId );
        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName );
        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName );
        public IReadOnlyCollection<KeySwitch> Find( ProductName productName );
        public IReadOnlyCollection<KeySwitch> Find( InstrumentName instrumentName );
        public IReadOnlyCollection<KeySwitch> FindAll();

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