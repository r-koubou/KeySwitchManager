using System;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Adding;

using RkHelper.Time;

namespace KeySwitchManager.Interactors.KeySwitch.Adding
{
    public class AddingInteractor : IAddingUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchFactory KeySwitchFactory { get; }
        private IArticulationFactory ArticulationFactory { get; }
        private IAddingPresenter Presenter { get; }

        public AddingInteractor(
            IKeySwitchRepository repository,
            IAddingPresenter presenter )
            : this(
                repository,
                IKeySwitchFactory.Default,
                IArticulationFactory.Default,
                presenter )
        {}

        public AddingInteractor(
            IKeySwitchRepository repository,
            IKeySwitchFactory keySwitchFactory,
            IArticulationFactory articulationFactory,
            IAddingPresenter presenter )
        {
            Repository          = repository;
            KeySwitchFactory    = keySwitchFactory;
            ArticulationFactory = articulationFactory;
            Presenter           = presenter;
        }

        public KeySwitchAddingResponse Execute( KeySwitchAddingRequest request )
        {
            var created = DateTimeHelper.NowUtc();
            var articulation = ArticulationFactory.Create(
                request.ArticulationName,
                request.MidiNoteOns,
                request.MidiControlChanges,
                request.MidiProgramChanges,
                request.ExtraData
            );
            var keySwitch = KeySwitchFactory.Create(
                Guid.NewGuid(),
                request.Author,
                request.Description,
                created,
                created,
                request.DeveloperName,
                request.ProductName,
                request.InstrumentName,
                new []{ articulation },
                request.ExtraData
            );

            Repository.Save( keySwitch );

            var response = new KeySwitchAddingResponse( true );
            Presenter.Complete( response );

            return response;
        }
    }
}