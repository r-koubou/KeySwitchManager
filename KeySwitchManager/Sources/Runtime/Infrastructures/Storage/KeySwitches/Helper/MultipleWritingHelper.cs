using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper
{
    public static class MultipleWritingHelper
    {
        public static void Write( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, string suffix, IObserver<string>? loggingSubject, Func<Stream, IKeySwitchWriter> writerFactory )
        {
            var multipleKeySwitches = KeySwitchHelper.GroupBy( keySwitches );

            foreach( var key in multipleKeySwitches.Keys )
            {
                var x = multipleKeySwitches[ key ];
                var baseDirectory = CreatePathHelper.CreateDirectoryTree( key.Item1, key.Item2, outputDirectory );

                foreach( var k in x )
                {
                    var filePath = CreatePathHelper.CreateFilePath( k, suffix, baseDirectory );
                    using var stream = filePath.OpenWriteStream();
                    using var fileWriter = writerFactory.Invoke( stream );

                    fileWriter.Write( new[] { k }, loggingSubject );
                }
            }
        }
    }
}
