using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;

namespace KeySwitchManager.Gateways.KeySwitches
{
    public interface IKeySwitchRepository
    {
        public int Count();
        public int Save( KeySwitch keySwitch );
        public int Delete( DeveloperName developerName, ProductName productName );
        public int Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public int Delete( EntityGuid guid );
        public int DeleteAll();
        public IReadOnlyCollection<KeySwitch> Find( EntityGuid guid );
        public IReadOnlyCollection<KeySwitch> Find( Guid guid );
        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName );
        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName );
        public IReadOnlyCollection<KeySwitch> Find( ProductName productName );
        public IReadOnlyCollection<KeySwitch> FindAll();
    }
}