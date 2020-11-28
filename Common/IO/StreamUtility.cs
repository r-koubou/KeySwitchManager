using System.IO;

namespace KeySwitchManager.Common.IO
{
    public static class StreamUtility
    {
        private const int WorkSize = 8192;
        private static readonly byte[] WorkBuffer = new byte[ WorkSize ];

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

    }
}