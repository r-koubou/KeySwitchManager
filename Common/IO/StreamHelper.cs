using System.IO;
using System.Reflection;

namespace KeySwitchManager.Common.IO
{
    public static class StreamHelper
    {
        private const int WorkSize = 8192;
        private static readonly byte[] WorkBuffer = new byte[ WorkSize ];

        #region Read, Write

        public static void ReadAllAndWrite( Stream source, Stream dest )
        {
            while( true )
            {
                var readByte = source.Read( WorkBuffer, 0, WorkBuffer.Length );

                if( readByte == 0 )
                {
                    break;
                }

                dest.Write( WorkBuffer, 0, readByte );
            }
        }

        public static void ReadBytes( Stream stream, byte[] buffer, int offset, int length )
        {
            int rest = length;
            int ofs = offset;

            while( rest > 0 )
            {
                var readByte = stream.Read( buffer, ofs, length );

                if( readByte == 0 )
                {
                    break;
                }

                ofs  += readByte;
                rest -= readByte;
            }
        }

        #endregion

        #region Manifest Resource

        /// <summary>
        /// Alias of Assembly.GetManifestResourceStream(Type, String)
        /// </summary>
        /// <typeparam name="TClass">Using for TClass's namespace</typeparam>
        /// <returns></returns>
        public static Stream? GetAssemblyResourceStream<TClass>( string resourceName )
            where TClass : class
        {
            var type = typeof( TClass );
            var asm = Assembly.GetAssembly( type );
            return asm?.GetManifestResourceStream( type, resourceName );
        }

        public static byte[] GetAssemblyResourceBytes<TClass>( string resourceName )
            where TClass : class
        {
            var type = typeof( TClass );
            var asm = Assembly.GetAssembly( type );
            using var stream = asm?.GetManifestResourceStream( type, resourceName );

            if( stream == null )
            {
                return new byte[ 0 ];
            }

            using var memoryStream = new MemoryStream();
            ReadAllAndWrite( stream, memoryStream );

            return memoryStream.ToArray();
        }

        #endregion

    }
}