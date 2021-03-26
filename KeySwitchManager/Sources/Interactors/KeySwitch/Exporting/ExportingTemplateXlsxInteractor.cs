using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Entity;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Gateways.KeySwitch;
using KeySwitchManager.UseCases.KeySwitches.Exporting;

namespace KeySwitchManager.Interactors.KeySwitch.Exporting
{
    public class ExportingTemplateXlsxInteractor : IExportingTemplateXlsxUseCase
    {
        private IKeySwitchSpreadSheetRepository Repository { get; }

        public ExportingTemplateXlsxInteractor( IKeySwitchSpreadSheetRepository repository )
        {
            Repository = repository;
        }

        public ExportingTemplateXlsxResponse Execute( ExportingTemplateXlsxRequest request )
        {
            #region Template keyswitch
            var entity = IKeySwitchFactory.Default.Create(
                Guid.NewGuid(),
                "Author",
                "Description",
                DateTime.Now,
                DateTime.Now,
                "Developer Name",
                "KeySwitchManager_Template",
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

            var result = Repository.Save( new []{ entity } );
            return new ExportingTemplateXlsxResponse( result );

        }
    }
}