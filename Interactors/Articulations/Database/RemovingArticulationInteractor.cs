using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Gateways.Articulations;
using ArticulationManager.Presenters.Articulations.Database;
using ArticulationManager.UseCases.Articulations.Database.Removing;

namespace ArticulationManager.Interactors.Articulations.Database
{
    public class RemovingArticulationInteractor : IRemovingArticulationUseCase
    {
        private IArticulationRepository Repository { get; }
        private IRemovingArticulationPresenter Presenter { get; }

        public RemovingArticulationInteractor(
            IArticulationRepository repository,
            IRemovingArticulationPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public void Execute( InputData inputData )
        {
            var developerName = new DeveloperName( inputData.DeveloperName );
            var productName = new ProductName( inputData.ProductName );

            Repository.Delete( developerName, productName );

            //TODO
            Presenter.Output( new OutputData() );
        }
    }
}