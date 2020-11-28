using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Common.IO;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Gateways.KeySwitches;

namespace KeySwitchManager.Xlsx.KeySwitches
{
    public class XlsxExportingRepository : IKeySwitchXlsxRepository
    {
        private const int InitialBufferSize = 1024 * 64;
        private byte[] XlsxBytes { get; }

        public XlsxExportingRepository( FilePath xlsxFilePath )
        {
            using var stream = new FileStream( xlsxFilePath.Path, FileMode.Create );
            using var memory = new MemoryStream( InitialBufferSize );

            StreamUtility.ReadAllAndWrite( stream, memory );
            XlsxBytes = memory.ToArray();
        }

        public XlsxExportingRepository( Stream stream )
        {
            using var memory = new MemoryStream( InitialBufferSize );

            StreamUtility.ReadAllAndWrite( stream, memory );
            XlsxBytes = memory.ToArray();
        }

        public void Dispose()
        {}

        public IReadOnlyCollection<KeySwitch> Load()
        {
            throw new System.NotImplementedException();
        }

        public bool Save( IReadOnlyCollection<KeySwitch> keySwitch ) =>
            throw new System.NotSupportedException();
    }
}