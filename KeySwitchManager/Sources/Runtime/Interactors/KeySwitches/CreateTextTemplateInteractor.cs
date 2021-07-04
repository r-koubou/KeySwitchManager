using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.KeySwitches.Models.Factory;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Factory;
using KeySwitchManager.UseCase.KeySwitches.Create.Text;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class CreateTextTemplateInteractor : ICreateTextTemplateUseCase
    {
        private IKeySwitchRepository OutputRepository { get; }
        private ICreateTextTemplatePresenter Presenter { get; }

        public CreateTextTemplateInteractor( IKeySwitchRepository outputRepository )
            : this( outputRepository, new ICreateTextTemplatePresenter.Null() )
        {}

        public CreateTextTemplateInteractor(
            IKeySwitchRepository outputRepository,
            ICreateTextTemplatePresenter presenter )
        {
            OutputRepository = outputRepository;
            Presenter        = presenter;
        }

        public CreateTextTemplateResponse Execute()
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

            return new CreateTextTemplateResponse();
        }
    }
}