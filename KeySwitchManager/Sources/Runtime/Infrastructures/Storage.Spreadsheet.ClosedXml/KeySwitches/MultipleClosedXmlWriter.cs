using System;
using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class MultipleClosedXmlWriter : IKeySwitchWriter
    {
        private const string Suffix = ".xlsx";

        public DirectoryPath OutputDirectory { get; }
        public bool LeaveOpen => false;

        public MultipleClosedXmlWriter( DirectoryPath outputDirectory )
        {
            OutputDirectory = outputDirectory;
        }

        public void Dispose() {}

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject = null )
        {
            var multipleKeySwitches = KeySwitchHelper.GroupBy( keySwitches );

            foreach( var key in multipleKeySwitches.Keys )
            {
                var x = multipleKeySwitches[ key ];
                var baseDirectory = CreatePathHelper.CreateDirectoryTree( key.Item1, key.Item2, OutputDirectory );

                foreach( var k in x )
                {
                    var filePath = CreatePathHelper.CreateFilePath( k, Suffix, baseDirectory );
                    using var stream = filePath.OpenWriteStream();
                    using var fileWriter = new ClosedXmlWriter( stream );

                    fileWriter.Write( new[] { k }, loggingSubject );
                }
            }
        }
    }
}
