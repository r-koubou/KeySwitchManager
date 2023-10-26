using System.IO;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public interface IContent
    {
        Stream GetContentStream();
    }
}
