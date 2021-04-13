using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Midi.Models;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Entities;
using KeySwitchManager.UseCase.KeySwitches.Create.Template;

namespace KeySwitchManager.Interactor.KeySwitches
{
    public class TemplateKeySwitchCreateInteractor : ITemplateKeySwitchCreateUseCase
    {
        private IKeySwitchRepository OutputRepository { get; }
        private ITemplateKeySwitchCreatePresenter Presenter { get; }

        public TemplateKeySwitchCreateInteractor( IKeySwitchRepository outputRepository )
            : this( outputRepository, new ITemplateKeySwitchCreatePresenter.Null() )
        {}

        public TemplateKeySwitchCreateInteractor(
            IKeySwitchRepository outputRepository,
            ITemplateKeySwitchCreatePresenter presenter )
        {
            OutputRepository = outputRepository;
            Presenter        = presenter;
        }

        public TemplateKeySwitchCreateResponse Execute()
        {
            var keySwitch = IKeySwitchFactory.Default.Create(
                Guid.NewGuid(),
                "Author",
                "Description",
                DateTime.Now,
                DateTime.Now,
                "Developer Name",
                "Product name",
                "Instrument name",
                new List<Articulation>
                {
                    IArticulationFactory.Default.Create(
                        "name",
                        new List<IMidiMessage>{ IMidiNoteOnFactory.Default.Create( 0, 100 )},
                        new List<IMidiMessage>{ IMidiControlChangeFactory.Default.Create( 1, 100 )},
                        new List<IMidiMessage>{ IMidiProgramChangeFactory.Default.Create( 2, 34 )},
                        new Dictionary<string, string>
                        {
                            { "extra1 key", "extra1 value" },
                            { "extra2 key", "extra2 value" },
                        }
                        ),
                },
                new Dictionary<string, string>
                {
                    { "extra1 key", "extra1 value" },
                    { "extra2 key", "extra2 value" },
                }
            );

            OutputRepository.Save( keySwitch );

            var flushed = OutputRepository.Flush();

            if( flushed == 0 )
            {
                Presenter.Message( $"No keyswitch(es) flushed to storage/repository ({OutputRepository.GetType()})" );
            }

            return new TemplateKeySwitchCreateResponse();
        }
    }
}