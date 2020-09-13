using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.UseCases.Articulations.Exporting.Text
{
    public interface IEntityTranslationService : IDataTranslationService<IEnumerable<Articulation>, IEnumerable<IText>>
    {}
}