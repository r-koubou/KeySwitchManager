using System;
using System.Collections.Generic;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Xml.Cubase
{
    public class MultipleCubaseWriter : IKeySwitchWriter
    {
        private const string Suffix = ".expressionmap";

        public DirectoryPath OutputDirectory { get; }
        private Encoding FileEncoding { get; }
        public bool LeaveOpen => false;

        public MultipleCubaseWriter( DirectoryPath outputDirectory ) : this( outputDirectory, Encoding.UTF8 ) {}

        public MultipleCubaseWriter( DirectoryPath outputDirectory, Encoding filEncoding )
        {
            OutputDirectory = outputDirectory;
            FileEncoding    = filEncoding;
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
                    using var fileWriter = new CubaseWriter( stream, FileEncoding );

                    fileWriter.Write( new[] { k }, loggingSubject );
                }
            }
        }
    }
}
