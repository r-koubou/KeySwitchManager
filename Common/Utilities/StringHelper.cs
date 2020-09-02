namespace ArticulationManager.Common.Utilities
{
    public static class StringHelper
    {
        public static bool IsNullOrTrimEmpty( string? text )
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