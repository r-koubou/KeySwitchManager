using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public interface IContentWriter
    {
        Task WriteAsync( IContent content );
    }
}
