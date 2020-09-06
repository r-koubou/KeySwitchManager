using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;

namespace ArticulationManager.Gateways.Articulations
{
    public interface IArticulationRepository
    {
        public void Save( Articulation articulation );
        public void Remove( Articulation articulation );
        public IReadOnlyList<Articulation> All();
        public IEnumerable<T> Find<T>( Expression<Func<T, bool>> predicate, int skip = 0, int limit = 2147483647 );
        public IEnumerable<Articulation> Find( DeveloperName developerName );
        public IEnumerable<Articulation> Find( ProductName productName );
    }
}