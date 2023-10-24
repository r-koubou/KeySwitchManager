using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public class CreateFileRequest
    {
        public IKeySwitchWriter Writer { get; }

        public CreateFileRequest( IKeySwitchWriter writer )
        {
            Writer = writer;
        }
    }
}