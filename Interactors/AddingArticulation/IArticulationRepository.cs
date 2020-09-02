using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Commons;

namespace ArticulationManager.Interactors.AddingArticulation
{
    public interface IArticulationRepository
    {
        public void Save( Articulation articulation );
        public void Remove( Articulation articulation );
        public IReadOnlyList<Articulation> All();
        public IEnumerable<Articulation> Query( EntityId id );
        public IEnumerable<Articulation> Query( DeveloperName id );
        public IEnumerable<Articulation> Query( ProductName id );
    }
}