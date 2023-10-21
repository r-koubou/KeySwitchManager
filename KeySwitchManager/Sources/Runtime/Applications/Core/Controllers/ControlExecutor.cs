using System;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Core.Views.LogView;

namespace KeySwitchManager.Applications.Core.Controllers
{
    public static class ControlExecutor
    {
        public static void Execute( Func<IController> controllerFactory, ILogTextView logTextView )
            => ExecuteAsync( controllerFactory, logTextView ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync( Func<IController> controllerFactory, ILogTextView logTextView )
        {
            try
            {
                using var controller = controllerFactory.Invoke();
                await ExecuteImplAsync( controller, logTextView );
            }
            catch( Exception exception )
            {
                logTextView.Append( exception.ToString() );
            }
        }

        private static async Task ExecuteImplAsync( IController controller, ILogTextView logTextView )
        {
            try
            {
                await controller.ExecuteAsync();
            }
            catch( Exception e )
            {
                logTextView.Append( e.ToString() );
            }
        }
    }
}
