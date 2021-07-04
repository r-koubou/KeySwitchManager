using System;
using System.Threading.Tasks;

using KeySwitchManager.Core.Applications.Views.LogView;

namespace KeySwitchManager.Core.Applications.Controllers
{
    public static class ControlExecutor
    {
        public static void Execute( Func<IController> controllerFactory, ILogTextView logTextView )
        {
            try
            {
                using var controller = controllerFactory.Invoke();
                ExecuteImpl( controller, logTextView );
            }
            catch( Exception exception )
            {
                logTextView.Append( exception.ToString() );
            }
        }

        private static void ExecuteImpl( IController controller, ILogTextView logTextView )
        {
            try
            {
                controller.Execute();
            }
            catch( Exception e )
            {
                logTextView.Append( e.ToString() );
            }
        }

        public static async Task ExecuteAsync( Func<IController> controllerFactory, ILogTextView logTextView )
        {
            await Task.Run( () => {
                Execute( controllerFactory, logTextView );
            } );
        }
    }
}
