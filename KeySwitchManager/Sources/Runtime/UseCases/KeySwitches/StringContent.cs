using System.IO;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public class StringContent : IContent
    {
        private readonly string data;

        public StringContent( string data )
        {
            this.data = data;
        }

        public Stream GetContentStream()
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter( memoryStream );
            writer.Write( data );
            writer.Flush();
            memoryStream.Seek( 0, SeekOrigin.Begin );

            return memoryStream;
        }
    }
}
