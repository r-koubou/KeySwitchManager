using System.IO;
using System.Threading.Tasks;

using RkHelper.Primitives;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public class BinaryContent : IContent
    {
        private readonly byte[] data;
        private readonly int offset;
        private readonly int length;

        public BinaryContent( byte[] data )
            : this( data, 0, data.Length ) {}

        public BinaryContent( byte[] data, int offset, int length )
        {
            ArrayHelper.ValidateArrayRange( data, offset, length );
            this.data   = data;
            this.offset = offset;
            this.length = length;
        }

        public Task<Stream> GetContentStreamAsync()
        {
            return Task.FromResult<Stream>( new MemoryStream( data, offset, length ) );
        }
    }
}
