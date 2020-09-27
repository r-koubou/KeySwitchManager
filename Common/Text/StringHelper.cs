using KeySwitchManager.Common.Exceptions;

namespace KeySwitchManager.Common.Text
{
    public static class StringHelper
    {
        public static bool IsNullOrTrimEmpty( params string[] texts )
        {
            foreach( var t in texts )
            {
                if( string.IsNullOrEmpty( t ) || t.Trim().Length == 0 )
                {
                    return true;
                }
            }

            return false;
        }

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

        public static bool Contains( string search, params string[] texts )
        {
            foreach( var i in texts )
            {
                if( i.Contains( search ) )
                {
                    return true;
                }
            }

            return false;
        }

    }
}