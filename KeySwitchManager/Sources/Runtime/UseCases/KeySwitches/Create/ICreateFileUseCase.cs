using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public interface ICreateFileUseCase
    {
        public CreateFileResponse Execute()
            => ExecuteAsync().GetAwaiter().GetResult();

        public Task<CreateFileResponse> ExecuteAsync();
    }
}
