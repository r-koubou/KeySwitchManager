using System.Diagnostics.CodeAnalysis;

namespace ArticulationManager.Utilities
{
    public static class StringHelper
    {
        public static bool IsNullOrTrimEmpty( [AllowNull] string text )
        {
            return string.IsNullOrEmpty( text ) || text.Trim().Length == 0;
        }

        public static bool ValidateNullOrTrimEmpty(  string text )
        {
            if( IsNullOrTrimEmpty( text ) )
            {
                throw new NullOrEmptyException();
            }
            return string.IsNullOrEmpty( text ) || text.Trim().Length == 0;
        }
    }
}