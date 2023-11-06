using System.Text;

namespace KeySwitchManager.Commons.Helpers
{
    public static class EncodingHelper
    {
        // ReSharper disable InconsistentNaming
        public static Encoding UTF8 => Encoding.UTF8;
        public static Encoding UTF8N { get; } = new UTF8Encoding( false );
        // ReSharper restore InconsistentNaming
    }
}
