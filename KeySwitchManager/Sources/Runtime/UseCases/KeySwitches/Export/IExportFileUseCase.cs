using System;
using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportFileUseCase
    {
        public ExportFileResponse Execute( ExportFileRequest request, IObserver<string>? loggingSubject = null )
            => ExecuteAsync( request ).GetAwaiter().GetResult();

        public Task<ExportFileResponse> ExecuteAsync( ExportFileRequest request, CancellationToken cancellationToken = default );
    }
}
