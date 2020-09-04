using ArticulationManager.Databases.Articulations.Model;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.Databases.Articulations.Service
{
    public class ArticulationTranslationService : IDataTranslationService<Articulation, ArticulationModel>
    {
        public ArticulationModel Translate( Articulation source )
        {
            return new ArticulationModel(
                source.DeveloperName.Value,
                source.ProductName.Value,
                source.ArticulationName.Value,
                source.ArticulationGroup.Value,
                source.ArticulationColor.Value
            );
        }
    }
}