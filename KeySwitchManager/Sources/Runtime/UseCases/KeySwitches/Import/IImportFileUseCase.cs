using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public interface IImportFileUseCase
    {
        public ImportFileResponse Execute( ImportFileRequest request )
            => ExecuteAsync( request, default ).GetAwaiter().GetResult();

        public Task<ImportFileResponse> ExecuteAsync( ImportFileRequest request, CancellationToken cancellationToken );
    }
}
