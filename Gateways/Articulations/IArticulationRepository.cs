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
        public void Delete( DeveloperName developerName );
        public void Delete( ProductName productName );
        public void Delete( ArticulationName articulationName );
        public void DeleteAll();
        public IEnumerable<Articulation> Find( DeveloperName developerName );
        public IEnumerable<Articulation> Find( ProductName productName );
        public IEnumerable<Articulation> Find( ArticulationName articulationName );
        public IEnumerable<Articulation> FindAll();
    }
}