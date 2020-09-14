using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;

namespace ArticulationManager.Gateways.Articulations
{
    public interface IKeySwitchRepository
    {
        public int Count();
        public void Save( KeySwitch keySwitch );
        public void Delete( DeveloperName developerName, ProductName productName );
        public void Delete( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public void Delete( KeySwitch keySwitch );
        public void DeleteAll();
        public IEnumerable<KeySwitch> Find( DeveloperName developerName, ProductName productName, InstrumentName instrumentName );
        public IEnumerable<KeySwitch> Find( DeveloperName developerName, ProductName productName );
        public IEnumerable<KeySwitch> Find( DeveloperName developerName );
        public IEnumerable<KeySwitch> Find( ProductName productName );
        public IEnumerable<KeySwitch> FindAll();
    }
}