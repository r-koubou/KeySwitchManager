using System.IO;

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

        public Stream GetContentStream()
        {
            return new MemoryStream( data, offset, length );
        }
    }
}
