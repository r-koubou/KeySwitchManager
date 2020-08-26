using System;

namespace ArticulationManager.Utilities
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