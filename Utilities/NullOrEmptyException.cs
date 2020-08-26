using System;

namespace ArticulationManager.Utilities
{
    public class NullOrEmptyException : Exception
    {
        public NullOrEmptyException()
        {}

        public NullOrEmptyException( string message ) : base( message )
        {}
    }
}