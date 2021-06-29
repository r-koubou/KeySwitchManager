using System;

namespace KeySwitchManager.AppCore.Controllers
{
    public interface IController : IDisposable
    {
        void Execute();
    }
}
