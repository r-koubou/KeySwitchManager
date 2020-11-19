using System;

namespace KeySwitchManager.Common.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException()
        {
        }

        public InvalidNameException( string variableName ) : base( $"{variableName} is invalid (null or empty?)" )
        {
        }
    }
}