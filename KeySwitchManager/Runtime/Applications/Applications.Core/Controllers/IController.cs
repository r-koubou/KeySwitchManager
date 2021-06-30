using System;

namespace Application.Core.Controllers
{
    public interface IController : IDisposable
    {
        void Execute();
    }
}
