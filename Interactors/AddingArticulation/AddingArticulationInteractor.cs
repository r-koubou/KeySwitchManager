using System;

using ArticulationManager.Domain.Articulations;
using ArticulationManager.UseCases.AddingArticulation;

namespace ArticulationManager.Interactors.AddingArticulation
{
    public class AddingArticulationInteractor : IAddingArticulationUseCase
    {
        private IArticulationRepository Repository { get; }
        private IArticulationFactory ArticulationFactory { get; }
        private IAddingArticulationPresenter Presenter { get; }

        public AddingArticulationInteractor(
            IArticulationRepository repository,
            IAddingArticulationPresenter presenter )
            : this(
                repository,
                new IArticulationFactory.DefaultFactory(),
                presenter )
        {}

        public AddingArticulationInteractor(
            IArticulationRepository repository,
            IArticulationFactory articulationFactory,
            IAddingArticulationPresenter presenter )
        {
            Repository          = repository;
            ArticulationFactory = articulationFactory;
            Presenter           = presenter;
        }

        public void Execute( InputData inputData )
        {
            var id = Guid.NewGuid().ToString();
            var entity = ArticulationFactory.Create(
                id,
                inputData.DeveloperName,
                inputData.ProductName,
                inputData.ArticulationName,
                inputData.ArticulationType,
                inputData.ArticulationGroup,
                inputData.ArticulationColor,
                inputData.MidiNoteOns,
                inputData.MidiControlChanges,
                inputData.MidiProgramChanges
            );

            Repository.Save( entity );

            //TODO
            Presenter.Output( new OutputData() );
        }
    }
}