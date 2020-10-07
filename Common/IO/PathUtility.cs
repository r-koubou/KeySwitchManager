using System;
using System.IO;

namespace KeySwitchManager.Common.IO
{
    public static class PathUtility
    {
        public static string GenerateFilePathWhenExist( string fileName, string baseDirectory, bool overwriteWhenExist )
        {
            var pathExtension = Path.GetExtension( fileName );
            var pathWithoutExtension = Path.GetFileNameWithoutExtension( fileName );
            var duplicate = 0;

            if( overwriteWhenExist )
            {
                return Path.Combine( baseDirectory, fileName );
            }

            var path = Path.Combine( baseDirectory, fileName );

            while( File.Exists( path ) )
            {
                if( duplicate == int.MaxValue )
                {
                    throw new InvalidOperationException( "too many duplicate files!");
                }
                duplicate++;
                path = Path.Combine( baseDirectory, $"{pathWithoutExtension} ({duplicate}){pathExtension}" );
            }

            return fileName;
        }

        public static void CreateDirectory( string directory )
        {
            if( !Directory.Exists( directory ) )
            {
                Directory.CreateDirectory( directory );
            }
        }
    }
}