using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
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

        public ExportingTemplateJsonInteractor(
            IExportingTextPresenter presenter,
            IKeySwitchListToJsonListText translator )
        {
            Presenter  = presenter;
            Translator = translator;
        }

        public ExportingTemplateAsTextResponse Execute()
        {
            var entity = new IKeySwitchFactory.Default().Create(
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
                    new IArticulationFactory.Default().Create(
                        "name",
                        ArticulationType.Default,
                        0,
                        0,
                        new List<IMessage>{ new INoteOnFactory.Default().Create( 0, 100 )},
                        new List<IMessage>{ new IControlChangeFactory.Default().Create( 1, 100 )},
                        new List<IMessage>{ new IProgramChangeFactory.Default().Create( 2, 34 )}
                        )
                }
            );

            var jsonText = Translator.Translate( new[] { entity } );
            return new ExportingTemplateAsTextResponse( jsonText.Value );
        }
    }
}