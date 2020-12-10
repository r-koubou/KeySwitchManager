using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting;

namespace KeySwitchManager.Interactors.KeySwitches.Exporting
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
            var entity = IKeySwitchFactory.Default.Create(
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
                        new Dictionary<string, string>()
                    ),
                },
                new Dictionary<string, string>()
            );

            var result = Repository.Save( new []{ entity } );
            return new ExportingTemplateXlsxResponse( result );

        }
    }
}