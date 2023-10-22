using System;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportFileUseCase
    {
        public ExportFileResponse Execute( ExportFileRequest request, IObserver<string>? loggingSubject = null )
            => ExecuteAsync( request, loggingSubject ).GetAwaiter().GetResult();

        public Task<ExportFileResponse> ExecuteAsync( ExportFileRequest request, IObserver<string>? loggingSubject );
    }
}
