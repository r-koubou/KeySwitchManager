using System;

namespace KeySwitchManager.Common.Exceptions
{
    public class ObjectIsNullException : Exception
    {
        public ObjectIsNullException() : base( "object is null" )
        {}

        public ObjectIsNullException( string message ) : base( message )
        {}
    }
}