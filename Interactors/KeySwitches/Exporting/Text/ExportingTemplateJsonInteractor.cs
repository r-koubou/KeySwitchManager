using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting.Text;

namespace KeySwitchManager.Interactors.KeySwitches.Exporting.Text
{
    public class ExportingTemplateJsonInteractor : IExportingTemplateAsTextUseCase
    {
        private IKeySwitchToText Translator { get; }
        private IExportingTextPresenter Presenter { get; }

        public ExportingTemplateJsonInteractor(
            IExportingTextPresenter presenter,
            IKeySwitchToText translator )
        {
            Presenter  = presenter;
            Translator = translator;
        }

        public void Execute()
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

            Presenter.Output( new OutputData( Translator.Translate( entity ) ) );
        }
    }
}