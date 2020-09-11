using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.UseCases.Articulations.Exporting.Text;

namespace ArticulationManager.Gateways.Json.Articulations
{
    public class EntitiesToJsonTranslator : IEntitiesToJsonTranslator
    {
        public string Translate( IEnumerable<Articulation> source )
        {
            throw new System.NotImplementedException();
        }
    }
}