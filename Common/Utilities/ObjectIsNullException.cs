using System;

namespace KeySwitchManager.Common.Utilities
{
    public class ObjectIsNullException : Exception
    {
        public ObjectIsNullException() : base( "object is null" )
        {}

        public ObjectIsNullException( string message ) : base( message )
        {}
    }
}