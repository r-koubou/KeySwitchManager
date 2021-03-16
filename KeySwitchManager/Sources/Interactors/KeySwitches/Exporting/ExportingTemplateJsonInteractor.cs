using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Entity;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting;
using KeySwitchManager.UseCases.KeySwitches.Translations;

namespace KeySwitchManager.Interactors.KeySwitches.Exporting
{
    public class ExportingTemplateJsonInteractor : IExportingTemplateAsTextUseCase
    {
        private IKeySwitchListToJsonListText Translator { get; }
        private IExportingTextPresenter Presenter { get; }

        public ExportingTemplateJsonInteractor( IKeySwitchListToJsonListText translator )
            : this( translator, new IExportingTextPresenter.Null() )
        {}

        public ExportingTemplateJsonInteractor(
            IKeySwitchListToJsonListText translator,
            IExportingTextPresenter presenter )
        {
            Presenter  = presenter;
            Translator = translator;
        }

        public ExportingTemplateAsTextResponse Execute()
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

            var jsonText = Translator.Translate( new[] { entity } );
            return new ExportingTemplateAsTextResponse( jsonText.Value );
        }
    }
}