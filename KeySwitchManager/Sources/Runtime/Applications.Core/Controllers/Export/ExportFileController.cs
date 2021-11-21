using System;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Export
{
    public class ExportFileController : IController
    {
        private IKeySwitchRepository SourceRepository { get; }
        private IKeySwitchWriter Writer { get; }
        private DeveloperName DeveloperName { get; }
        private ProductName ProductName { get; }
        private InstrumentName InstrumentName { get; }
        private IExportFilePresenter Presenter { get; }
        private IObserver<string> LoggingObserver { get; }


        // TODO 引数で IKeySwitchWriter を受け取ればポリモーフィズムできるが、単一ファイルのみの出力になる。
        // → そもそも IKeySwitchWriter == 単一ファイルのみ、に制限をしていないので MultipleFileWriter的クラスを実装すれば良い？
        public ExportFileController(
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName,
            IKeySwitchRepository sourceRepository,
            IKeySwitchWriter writer,
            IExportFilePresenter presenter,
            IObserver<string> loggingObserver )
        {
            DeveloperName    = developerName;
            ProductName      = productName;
            InstrumentName   = instrumentName;
            SourceRepository = sourceRepository;
            Writer           = writer;
            Presenter        = presenter;
            LoggingObserver  = loggingObserver;
        }

        public void Dispose()
        {
            Disposer.Dispose( SourceRepository );

            if( !Writer.LeaveOpen )
            {
                Disposer.Dispose( Writer );
            }
        }

        public void Execute()
        {
            var interactor = new ExportFileInteractor(
                SourceRepository,
                Writer,
                Presenter
            );

            var response = interactor.Execute(
                new ExportFileRequest(
                    DeveloperName.Value,
                    ProductName.Value,
                    InstrumentName.Value
                )
            );

            Presenter.Complete( response );
        }
    }
}
