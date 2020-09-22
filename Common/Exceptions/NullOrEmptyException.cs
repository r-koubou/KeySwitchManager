using System;

namespace KeySwitchManager.Common.Exceptions
{
    public class NullOrEmptyException : Exception
    {
        public NullOrEmptyException()
        {}

        public NullOrEmptyException( string message ) : base( message )
        {}
    }
}