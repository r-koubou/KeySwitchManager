using System;
using System.Threading.Tasks;

namespace KeySwitchManager.Applications.Core.Controllers
{
    public interface IController : IDisposable
    {
        void Execute()
            => ExecuteAsync().GetAwaiter().GetResult();

        Task ExecuteAsync();
    }
}
