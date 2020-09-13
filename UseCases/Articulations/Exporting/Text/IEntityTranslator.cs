using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.Translations;

namespace ArticulationManager.UseCases.Articulations.Exporting.Text
{
    public interface IEntityTranslator : IDataTranslator<IEnumerable<Articulation>, IEnumerable<IText>>
    {}
}