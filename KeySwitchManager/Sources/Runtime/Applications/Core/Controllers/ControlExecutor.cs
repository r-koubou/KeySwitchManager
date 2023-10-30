using System;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Core.Views.LogView;

namespace KeySwitchManager.Applications.Core.Controllers
{
    public static class ControlExecutor
    {
        public static void Execute( Func<IController> controllerFactory, ILogTextView logTextView )
            => ExecuteAsync( controllerFactory, logTextView, default ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync( Func<IController> controllerFactory, ILogTextView logTextView, CancellationToken cancellationToken )
        {
            try
            {
                using var controller = controllerFactory.Invoke();
                await ExecuteImplAsync( controller, logTextView, cancellationToken );
            }
            catch( OperationCanceledException )
            {
                throw;
            }
            catch( Exception exception )
            {
                logTextView.Append( exception.ToString() );
            }
        }

        private static async Task ExecuteImplAsync( IController controller, ILogTextView logTextView, CancellationToken cancellationToken )
        {
            try
            {
                await controller.ExecuteAsync( cancellationToken );
            }
            catch( Exception e )
            {
                logTextView.Append( e.ToString() );
            }
        }
    }
}
