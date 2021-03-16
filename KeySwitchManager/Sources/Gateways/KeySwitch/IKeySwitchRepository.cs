using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitch.Value;

namespace KeySwitchManager.Gateways.KeySwitch
{
    public interface IKeySwitchRepository
    {
        public int Count();
        public SaveResult Save( Domain.KeySwitches.KeySwitch keySwitch );
        public int Delete( DeveloperName developerName, ProductName productName );
        public int Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public int Delete( EntityGuid guid );
        public int DeleteAll();
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Find( EntityGuid guid );
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Find( Guid guid );
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Find( DeveloperName developerName, ProductName productName );
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Find( DeveloperName developerName );
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Find( ProductName productName );
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> FindAll();
    }
}