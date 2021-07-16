using System;

namespace KeySwitchManager.Applications.Core.Controllers
{
    public interface IController : IDisposable
    {
        void Execute();
    }
}
