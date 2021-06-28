using System;

namespace KeySwitchManager.GuiCore.Controllers
{
    public interface IController : IDisposable
    {
        void Execute();
    }
}
