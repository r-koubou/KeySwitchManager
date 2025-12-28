using AppKit;

using Foundation;

namespace KeySwitchManager.Xamarin.Mac
{
    [Register( "AppDelegate" )]
    public class AppDelegate : NSApplicationDelegate
    {
        // ReSharper disable once EmptyConstructor
        public AppDelegate()
        {
            /*caret*/
        }

        public override void DidFinishLaunching( NSNotification notification )
        {
            // Insert code here to initialize your application
        }

        public override void WillTerminate( NSNotification notification )
        {
            // Insert code here to tear down your application
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed( NSApplication sender )
            => true;
    }
}
