using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.KeySwitches.Models.Factory;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Factory;
using KeySwitchManager.UseCase.KeySwitches.Create.Spreadsheet;

namespace KeySwitchManager.Interactor.KeySwitches
{
    public class CreateSpreadsheetTemplateInteractor : ICreateSpreadsheetTemplateUseCase
    {
        private IKeySwitchRepository OutputRepository { get; }
        private ICreateSpreadsheetTemplatePresenter Presenter { get; }

        public CreateSpreadsheetTemplateInteractor( IKeySwitchRepository outputRepository ) :
            this( outputRepository, new ICreateSpreadsheetTemplatePresenter.Null() )
        {}

        public CreateSpreadsheetTemplateInteractor(
            IKeySwitchRepository outputRepository,
            ICreateSpreadsheetTemplatePresenter presenter )
        {
            OutputRepository = outputRepository;
            Presenter        = presenter;
        }

        public CreateSpreadsheetTemplateResponse Execute( CreateSpreadsheetTemplateRequest request )
        {
            #region Template keyswitch
            var entity = IKeySwitchFactory.Default.Create(
                Guid.NewGuid(),
                "Author",
                "Description",
                DateTime.Now,
                DateTime.Now,
                "Developer Name",
                "(ProductName)",
                "Instrument name",
                new List<Articulation>
                {
                    IArticulationFactory.Default.Create(
                        "IDLE",
                        new List<IMidiChannelVoiceMessage>(),
                        new List<IMidiChannelVoiceMessage>(),
                        new List<IMidiChannelVoiceMessage>(),
                        new Dictionary<string, string>()
                    ),
                    IArticulationFactory.Default.Create(
                        "Power Chord",
                        new List<IMidiChannelVoiceMessage>{ IMidiNoteOnFactory.Default.Create( 0, 100 )},
                        new List<IMidiChannelVoiceMessage>{ IMidiControlChangeFactory.Default.Create( 1, 100 )},
                        new List<IMidiChannelVoiceMessage>{ IMidiProgramChangeFactory.Default.Create( 34 )},
                        new Dictionary<string, string>()
                    ),
                },
                new Dictionary<string, string>()
            );
            #endregion

            OutputRepository.Save( entity );

            var flushed = OutputRepository.Flush();

            if( flushed == 0 )
            {
                Presenter.Present( $"No keyswitch(es) flushed to storage/repository ({OutputRepository.GetType()})" );
            }

            return new CreateSpreadsheetTemplateResponse( true );
        }
    }
}