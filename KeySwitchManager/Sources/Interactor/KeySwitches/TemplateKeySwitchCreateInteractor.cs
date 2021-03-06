using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.KeySwitches.Models.Factory;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Factory;
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
                        new List<IMidiChannelVoiceMessage>{ IMidiNoteOnFactory.Default.Create( 0, 100 )},
                        new List<IMidiChannelVoiceMessage>{ IMidiControlChangeFactory.Default.Create( 1, 100 )},
                        new List<IMidiChannelVoiceMessage>{ IMidiProgramChangeFactory.Default.Create( 23 )},
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
                Presenter.Present( $"No keyswitch(es) flushed to storage/repository ({OutputRepository.GetType()})" );
            }

            return new TemplateKeySwitchCreateResponse();
        }
    }
}