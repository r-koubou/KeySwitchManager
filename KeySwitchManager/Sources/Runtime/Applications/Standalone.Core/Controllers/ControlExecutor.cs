using System;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers
{
    public static class ControlExecutor
    {
        public static void Execute( Func<IController> controllerFactory, ILogTextView logTextView )
            => ExecuteAsync( controllerFactory, logTextView ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync( Func<IController> controllerFactory, ILogTextView logTextView, CancellationToken cancellationToken = default )
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

        private static async Task ExecuteImplAsync( IController controller, ILogTextView logTextView, CancellationToken cancellationToken = default )
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
