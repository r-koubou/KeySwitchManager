using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting.Text;
using KeySwitchManager.UseCases.KeySwitches.Translations;

namespace KeySwitchManager.Interactors.KeySwitches.Exporting.Text
{
    public class ExportingJsonInteractor : IExportingTextUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchListToJsonListText Translator { get; }
        private IExportingTextPresenter Presenter { get; }

        public ExportingJsonInteractor(
            IKeySwitchRepository repository,
            IKeySwitchListToJsonListText translator,
            IExportingTextPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
            Translator = translator;
        }

        public ExportingTextResponse Execute( ExportingTextRequest request )
        {
            #region By Guid
            if( request.Guid != default )
            {
                //Presenter.Present( $"Finding keyswitch: Guid={request.Guid}" );
                return new ExportingTextResponse(
                    Translator.Translate(
                        Repository.Find(
                            new EntityGuid( request.Guid )
                        )
                    )
                )
                {
                    Found = true
                };
            }
            #endregion

            #region By Developer, Product, Instrument
            if( !string.IsNullOrEmpty( request.DeveloperName ) &&
                !string.IsNullOrEmpty( request.ProductName ) &&
                !string.IsNullOrEmpty( request.InstrumentName ) )
            {
                //Presenter.Present( $"Finding keyswitch: Developer={request.DeveloperName}, Product={request.ProductName}, InstrumentName={request.InstrumentName}" );
                return new ExportingTextResponse(
                    Translator.Translate(
                        Repository.Find(
                            new DeveloperName( request.DeveloperName ),
                            new ProductName( request.ProductName ),
                            new InstrumentName( request.InstrumentName )
                        ))
                )
                {
                    Found = true
                };
            }
            #endregion

            #region By Developer, Product
            if( !string.IsNullOrEmpty( request.DeveloperName ) &&
                !string.IsNullOrEmpty( request.ProductName ) )
            {
                //Presenter.Present( $"Finding keyswitch: Developer={request.DeveloperName}, Product={request.ProductName}" );
                return new ExportingTextResponse(
                    Translator.Translate(
                        Repository.Find(
                            new DeveloperName( request.DeveloperName ),
                            new ProductName( request.ProductName )
                        )
                    )
                )
                {
                    Found = true
                };
            }
            #endregion

            #region By Developer
            if( !string.IsNullOrEmpty( request.DeveloperName ) )
            {
                Presenter.Present( $"Finding keyswitch: Developer={request.DeveloperName}" );
                return new ExportingTextResponse(
                    Translator.Translate(
                        Repository.Find( new DeveloperName( request.DeveloperName ) )
                    )
                )
                {
                    Found = true
                };
            }
            #endregion

            return new ExportingTextResponse( new PlainText( "" ) );
        }
    }
}