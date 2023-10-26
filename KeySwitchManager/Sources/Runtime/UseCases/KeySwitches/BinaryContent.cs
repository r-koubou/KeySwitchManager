using System.IO;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public class BinaryContent : IContent
    {
        private readonly byte[] data;

        public BinaryContent( byte[] data )
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
