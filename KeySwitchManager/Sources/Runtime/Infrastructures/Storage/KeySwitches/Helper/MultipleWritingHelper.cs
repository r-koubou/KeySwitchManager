using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper
{
    public static class MultipleWritingHelper
    {
        [Obsolete]
        public static void Write( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, string suffix, IObserver<string>? loggingSubject, Func<Stream, IKeySwitchWriter> writerFactory )
        {
            foreach( var x in keySwitches )
            {
                var filePath = CreatePathHelper.CreateFilePath( x, suffix, outputDirectory );

                // Some writers require Read/Write access stream
                using var stream = filePath.OpenStream( FileMode.Create, FileAccess.ReadWrite );
                using var fileWriter = writerFactory.Invoke( stream );

                fileWriter.Write( new[] { x }, loggingSubject );
            }

            loggingSubject?.OnNext( $"{nameof(MultipleWritingHelper)} : {keySwitches.Count} records has been written" );
        }

        public static void Write( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, string suffix, IObserver<string>? logging, Action<Stream, KeySwitch, IObserver<string>?> write )
        {
            foreach( var x in keySwitches )
            {
                var filePath = CreatePathHelper.CreateFilePath( x, suffix, outputDirectory );

                // Some writers require Read/Write access stream
                using var stream = filePath.OpenStream( FileMode.Create, FileAccess.ReadWrite );
                write.Invoke( stream, x, logging );
            }

            logging?.OnNext( $"{nameof(MultipleWritingHelper)} : {keySwitches.Count} records has been written" );
        }

    }
}
