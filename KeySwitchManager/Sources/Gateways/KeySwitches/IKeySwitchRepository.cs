using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Values;
using KeySwitchManager.Gateways.KeySwitches.Values;

namespace KeySwitchManager.Gateways.KeySwitches
{
    public interface IKeySwitchRepository
    {
        public int Count();
        public SaveResult Save( KeySwitch keySwitch );
        public int Delete( DeveloperName developerName, ProductName productName );
        public int Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public int Delete( KeySwitchId guid );
        public int DeleteAll();
        public IReadOnlyCollection<KeySwitch> Find( KeySwitchId keySwitchId );
        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName, ProductName productName );
        public IReadOnlyCollection<KeySwitch> Find( DeveloperName developerName );
        public IReadOnlyCollection<KeySwitch> Find( ProductName productName );
        public IReadOnlyCollection<KeySwitch> FindAll();
    }
}