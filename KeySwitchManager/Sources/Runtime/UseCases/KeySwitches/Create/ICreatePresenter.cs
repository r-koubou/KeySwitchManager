using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Boundaries;
using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public interface ICreatePresenter : IOutputPort<CreateResponse>
    {
        public static readonly ICreatePresenter Null = new NullImpl();
        public static readonly ICreatePresenter DefaultConsole = new ConsoleImpl();

        private class NullImpl : ICreatePresenter
        {
            public async Task HandleAsync( CreateResponse response, CancellationToken cancellationToken = default )
            {
                await Task.CompletedTask;
            }
        }

        private class ConsoleImpl : ICreatePresenter
        {
            public async Task HandleAsync( CreateResponse response, CancellationToken cancellationToken = default )
            {
                if( response.Result )
                {
                    System.Console.WriteLine( $"Created : {response.Response}" );
                }
                else
                {
                    System.Console.WriteLine( "Failed to create." );
                    System.Console.WriteLine( response.Error?.StackTrace );
                }

                await Task.CompletedTask;
            }
        }
    }
}
