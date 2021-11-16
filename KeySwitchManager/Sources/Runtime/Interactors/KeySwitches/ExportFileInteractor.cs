using System;
using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class ExportFileInteractor : IExportFileUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchWriter Writer { get; }
        private IExportFilePresenter Presenter { get; }

        public ExportFileInteractor(
            IKeySwitchRepository repository,
            IKeySwitchWriter writer ) :
            this( repository, writer, new IExportFilePresenter.Null() )
        {}

        public ExportFileInteractor(
            IKeySwitchRepository repository,
            IKeySwitchWriter writer,
            IExportFilePresenter presenter )
        {
            Repository = repository;
            Writer     = writer;
            Presenter  = presenter;
        }

        public ExportFileResponse Execute( ExportFileRequest request, IObserver<string>? loggingSubject = null )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            var queryResult = SearchHelper.Search(
                Repository,
                developerName,
                productName,
                instrumentName
            );

            if( queryResult.Any() )
            {
                Writer.Write( queryResult, loggingSubject );
                return new ExportFileResponse( queryResult );
            }

            Presenter.Present( $"No keyswitch(es) found. ({nameof( developerName )}={developerName}, {nameof( productName )}={productName}, {nameof( instrumentName )}={instrumentName})" );
            return new ExportFileResponse( new List<KeySwitch>() );
        }
    }
}