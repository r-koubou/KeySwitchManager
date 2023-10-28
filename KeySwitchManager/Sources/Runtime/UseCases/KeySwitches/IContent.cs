using System.IO;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public interface IContent
    {
        Stream GetContentStream()
            => GetContentStreamAsync().GetAwaiter().GetResult();

        Task<Stream> GetContentStreamAsync();
    }
}
