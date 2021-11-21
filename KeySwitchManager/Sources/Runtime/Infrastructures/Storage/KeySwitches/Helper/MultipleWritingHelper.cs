using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper
{
    public static class MultipleWritingHelper
    {
        public static void Write( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, string suffix, IObserver<string>? loggingSubject, Func<Stream, IKeySwitchWriter> writerFactory )
        {
            foreach( var x in keySwitches )
            {
                var filePath = CreatePathHelper.CreateFilePath( x, suffix, outputDirectory );
                using var stream = filePath.OpenWriteStream();
                using var fileWriter = writerFactory.Invoke( stream );

                fileWriter.Write( new[] { x }, loggingSubject );
            }

            loggingSubject?.OnNext( $"{nameof(MultipleWritingHelper)} : {keySwitches.Count} records has been written" );
        }
    }
}
