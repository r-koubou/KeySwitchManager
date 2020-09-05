using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;

namespace ArticulationManager.Gateways.Articulations
{
    public interface IArticulationRepository
    {
        public void Save( Articulation articulation );
        public void Remove( Articulation articulation );
        public IReadOnlyList<Articulation> All();
        public IEnumerable<Articulation> Find( DeveloperName developerName );
        public IEnumerable<Articulation> Find( ProductName productName );
    }
}