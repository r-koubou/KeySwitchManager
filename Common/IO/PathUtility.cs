using System;
using System.IO;

namespace KeySwitchManager.Common.IO
{
    public static class PathUtility
    {
        public static string GenerateFilePathWhenExist( string path, string baseDirectory )
        {
            var pathExtension = Path.GetExtension( path );
            var pathWithoutExtension = Path.GetFileNameWithoutExtension( path );
            var duplicate = 0;

            while( File.Exists( path ) )
            {
                if( duplicate == int.MaxValue )
                {
                    throw new InvalidOperationException( "too many duplicate files!");
                }
                duplicate++;
                path = Path.Combine( baseDirectory, $"{pathWithoutExtension} ({duplicate}){pathExtension}" );
            }

            return path;
        }
    }
}