using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Common.IO;
using KeySwitchManager.Common.Text;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Xlsx.KeySwitches.Services;
using KeySwitchManager.Xlsx.KeySwitches.Translators;

namespace KeySwitchManager.Xlsx.KeySwitches
{
    public class XlsxExportingRepository : IKeySwitchXlsxRepository
    {
        public class KeySwitchInfo
        {
            public string DeveloperName { get; }
            public string ProductName { get; }
            public string Author { get; }
            public string Description { get; }

            protected KeySwitchInfo(
                string developerName,
                string productName,
                string author = "",
                string description = "" )
            {
                StringHelper.ValidateNullOrTrimEmpty( developerName );
                StringHelper.ValidateNullOrTrimEmpty( productName );
                StringHelper.ValidateNull( author );
                StringHelper.ValidateNull( description );

                DeveloperName = developerName;
                ProductName   = productName;
                Author        = author;
                Description   = description;
            }
        }

        private const int InitialBufferSize = 1024 * 64;
        private byte[] XlsxBytes { get; }
        private KeySwitchInfo Info { get; }

        public XlsxExportingRepository( FilePath xlsxFilePath, KeySwitchInfo info )
        {
            using var stream = new FileStream( xlsxFilePath.Path, FileMode.Create );
            using var memory = new MemoryStream( InitialBufferSize );

            StreamHelper.ReadAllAndWrite( stream, memory );
            XlsxBytes = memory.ToArray();
            Info      = info;
        }

        public XlsxExportingRepository( Stream stream, KeySwitchInfo info )
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
            var workBook = XlsxWorkBookParsingService.Parse( XlsxBytes );
            var translator = new XlsxToKeySwitches(
                Info.DeveloperName,
                Info.ProductName,
                Info.Author,
                Info.Description
            );

            return  translator.Translate( workBook );
        }

        public bool Save( IReadOnlyCollection<KeySwitch> keySwitch ) =>
            throw new NotSupportedException();
    }
}