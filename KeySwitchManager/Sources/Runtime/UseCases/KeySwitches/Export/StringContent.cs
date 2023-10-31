using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class StringContent : IContent
    {
        private readonly string data;
        private readonly Encoding encoding;

        public StringContent( string data ) : this( data, Encoding.UTF8 ) {}

        public StringContent( string data, Encoding encoding )
        {
            this.data     = data;
            this.encoding = encoding;
        }

        public async Task<Stream> GetContentStreamAsync()
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter( memoryStream );
            await writer.WriteAsync( data );
            await writer.FlushAsync();
            memoryStream.Seek( 0, SeekOrigin.Begin );

            return memoryStream;
        }
    }
}
