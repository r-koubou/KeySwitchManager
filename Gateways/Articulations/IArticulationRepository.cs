using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;

namespace ArticulationManager.Gateways.Articulations
{
    public interface IArticulationRepository
    {
        public int Count();
        public void Save( Articulation articulation );
        public void Delete( Articulation articulation );
        public void DeleteMany<T>( Expression<Func<T, bool>> predicate );
        public IEnumerable<T> Find<T>( Expression<Func<T, bool>> predicate, int skip = 0, int limit = 2147483647 );
        public IEnumerable<Articulation> Find( DeveloperName developerName );
        public IEnumerable<Articulation> Find( ProductName productName );
        public IEnumerable<Articulation> FindAll();
    }
}