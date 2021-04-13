using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Midi.Models;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Entities;
using KeySwitchManager.UseCase.KeySwitches.Create.SpreadsheetTemplate;

namespace KeySwitchManager.Interactor.KeySwitches
{
    public class SpreadsheetTemplateExportInteractor : ISpreadsheetTemplateExportUseCase
    {
        private IKeySwitchRepository OutputRepository { get; }
        private ISpreadsheetTemplateExportPresenter Presenter { get; }

        public SpreadsheetTemplateExportInteractor( IKeySwitchRepository outputRepository ) :
            this( outputRepository, new ISpreadsheetTemplateExportPresenter.Null() )
        {}

        public SpreadsheetTemplateExportInteractor(
            IKeySwitchRepository outputRepository,
            ISpreadsheetTemplateExportPresenter presenter )
        {
            OutputRepository = outputRepository;
            Presenter        = presenter;
        }

        public SpreadsheetTemplateExportResponse Execute( SpreadsheetTemplateExportRequest request )
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
                        new List<IMidiMessage>(),
                        new List<IMidiMessage>(),
                        new List<IMidiMessage>(),
                        new Dictionary<string, string>()
                    ),
                    IArticulationFactory.Default.Create(
                        "Power Chord",
                        new List<IMidiMessage>{ IMidiNoteOnFactory.Default.Create( 0, 100 )},
                        new List<IMidiMessage>{ IMidiControlChangeFactory.Default.Create( 1, 100 )},
                        new List<IMidiMessage>{ IMidiProgramChangeFactory.Default.Create( 2, 34 )},
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
                Presenter.Message( $"No keyswitch(es) flushed to storage/repository ({OutputRepository.GetType()})" );
            }

            return new SpreadsheetTemplateExportResponse( true );
        }
    }
}