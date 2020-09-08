using ArticulationManager.Common.Utilities;
using ArticulationManager.Databases.LiteDB.Articulations.Model;
using ArticulationManager.Domain.Articulations;
using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Services;

namespace ArticulationManager.Databases.LiteDB.Articulations.Service
{
    public class ArticulationModelTranslationService : IDataTranslationService<ArticulationModel, Articulation>
    {
        public Articulation Translate( ArticulationModel source )
        {
            return new IArticulationFactory.DefaultFactory().Create(
                source.Id,
                source.Created,
                source.LastUpdated,
                source.DeveloperName,
                source.ProductName,
                source.ArticulationName,
                EnumHelper.Parse<ArticulationType>( source.ArticulationType ),
                source.ArticulationGroup,
                source.ArticulationColor
            );
        }
    }
}