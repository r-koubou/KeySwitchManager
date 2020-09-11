using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Gateways.Articulations;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Removing;

namespace ArticulationManager.Interactors.Articulations.Removing
{
    public class RemovingInteractor : IRemovingUseCase
    {
        private IArticulationRepository Repository { get; }
        private IRemovingPresenter Presenter { get; }

        public RemovingInteractor(
            IArticulationRepository repository,
            IRemovingPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public void Execute( InputData inputData )
        {
            var developerName = new DeveloperName( inputData.DeveloperName );
            var productName = new ProductName( inputData.ProductName );

            Repository.Delete( developerName, productName );

            Presenter.Output( new OutputData( true ) );
        }
    }
}