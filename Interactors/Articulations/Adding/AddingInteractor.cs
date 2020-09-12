using System;

using ArticulationManager.Common.Utilities;
using ArticulationManager.Domain.Articulations;
using ArticulationManager.Gateways.Articulations;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Adding;

namespace ArticulationManager.Interactors.Articulations.Adding
{
    public class AddingInteractor : IAddingUseCase
    {
        private IArticulationRepository Repository { get; }
        private IArticulationFactory ArticulationFactory { get; }
        private IAddingPresenter Presenter { get; }

        public AddingInteractor(
            IArticulationRepository repository,
            IAddingPresenter presenter )
            : this(
                repository,
                new IArticulationFactory.Default(),
                presenter )
        {}

        public AddingInteractor(
            IArticulationRepository repository,
            IArticulationFactory articulationFactory,
            IAddingPresenter presenter )
        {
            Repository          = repository;
            ArticulationFactory = articulationFactory;
            Presenter           = presenter;
        }

        public void Execute( InputData inputData )
        {
            var created = DateTimeHelper.NowUtc();
            var entity = ArticulationFactory.Create(
                Guid.NewGuid(),
                created,
                created,
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

            Presenter.Output( new OutputData( true ) );
        }
    }
}