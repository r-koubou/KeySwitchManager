using System;

namespace KeySwitchManager.GuiCore.Sources.Controllers
{
    public interface IController : IDisposable
    {
        void Execute();
    }
}
