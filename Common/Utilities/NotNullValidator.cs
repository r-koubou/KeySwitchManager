using System.Diagnostics.CodeAnalysis;

namespace ArticulationManager.Common.Utilities
{
    public static class NotNullValidator
    {
        public static void Validate<T>( [AllowNull] T obj )
        {
            if( obj == null )
            {
                throw new ObjectIsNullException();
            }
        }
    }
}