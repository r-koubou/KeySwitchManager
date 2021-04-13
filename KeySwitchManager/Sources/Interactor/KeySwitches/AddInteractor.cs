using System;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Add;

using RkHelper.Time;

namespace KeySwitchManager.Interactor.KeySwitches
{
    public class AddInteractor : IAddUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchFactory KeySwitchFactory { get; } = IKeySwitchFactory.Default;
        private IArticulationFactory ArticulationFactory { get; } = IArticulationFactory.Default;
        private IAddPresenter Presenter { get; }

        public AddInteractor( IKeySwitchRepository repository )
            : this( repository, new IAddPresenter.Null() )
        {}

        public AddInteractor(
            IKeySwitchRepository repository,
            IAddPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public AddingResponse Execute( AddRequest request )
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

            var response = new AddingResponse( true );
            Presenter.Complete( response );

            return response;
        }
    }
}