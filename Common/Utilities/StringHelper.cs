namespace ArticulationManager.Common.Utilities
{
    public static class StringHelper
    {
        public static bool IsNullOrTrimEmpty( string? text )
        {
            return string.IsNullOrEmpty( text ) || text.Trim().Length == 0;
        }

        public static void ValidateNullOrTrimEmpty(  string text )
        {
            if( IsNullOrTrimEmpty( text ) )
            {
                throw new NullOrEmptyException();
            }
        }

        public static void ValidateNull(  string text )
        {
            if( text == null )
            {
                throw new NullOrEmptyException();
            }
        }

    }
}