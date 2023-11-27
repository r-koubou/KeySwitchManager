using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Boundaries;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public interface ICreatePresenter : IOutputPort<CreateResponse>
    {
        public static readonly ICreatePresenter Null = new NullImpl();

        private class NullImpl : ICreatePresenter
        {
            public Task HandleAsync( CreateResponse response, CancellationToken cancellationToken = default )
            {
                return Task.CompletedTask;
            }
        }

        public class Console : ICreatePresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }

            public void Message( string message )
            {
                System.Console.WriteLine( message );
            }

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
