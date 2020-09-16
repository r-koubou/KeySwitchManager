using System;

namespace KeySwitchManager.Common.Utilities
{
    public class NullOrEmptyException : Exception
    {
        public NullOrEmptyException()
        {}

        public NullOrEmptyException( string message ) : base( message )
        {}
    }
}