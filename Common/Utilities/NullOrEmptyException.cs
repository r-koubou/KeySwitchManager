using System;

namespace ArticulationManager.Common.Utilities
{
    public class NullOrEmptyException : Exception
    {
        public NullOrEmptyException()
        {}

        public NullOrEmptyException( string message ) : base( message )
        {}
    }
}