using System;

namespace KeySwitchManager.Core.Applications.Controllers
{
    public interface IController : IDisposable
    {
        void Execute();
    }
}
