using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Common.IO;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Xlsx.KeySwitches.Helpers;
using KeySwitchManager.Xlsx.KeySwitches.Translators.ToKeySwitch;

namespace KeySwitchManager.Xlsx.KeySwitches
{
    public class XlsxImportingRepository : IKeySwitchSpreadSheetRepository
    {
        private const int InitialBufferSize = 1024 * 64;
        private byte[] XlsxBytes { get; }
        private KeySwitchInfo Info { get; }

        public XlsxImportingRepository( FilePath xlsxFilePath, KeySwitchInfo info )
        {
            using var stream = new FileStream( xlsxFilePath.Path, FileMode.Open );
            using var memory = new MemoryStream( InitialBufferSize );

            StreamHelper.ReadAllAndWrite( stream, memory );
            XlsxBytes = memory.ToArray();
            Info      = info;
        }

        public XlsxImportingRepository( Stream stream, KeySwitchInfo info )
        {
            using var memory = new MemoryStream( InitialBufferSize );

            StreamHelper.ReadAllAndWrite( stream, memory );
            XlsxBytes = memory.ToArray();
            Info      = info;
        }

        public void Dispose()
        {}

        public IReadOnlyCollection<KeySwitch> Load()
        {
            var workBook = XlsxWorkBookParsingHelper.Parse( XlsxBytes );
            var translator = new XlsxToKeySwitches(
                Info.DeveloperName,
                Info.ProductName,
                Info.Author,
                Info.Description
            );

            return  translator.Translate( workBook );
        }

        public bool Save( IReadOnlyCollection<KeySwitch> keySwitches ) =>
            throw new NotSupportedException();
    }
}