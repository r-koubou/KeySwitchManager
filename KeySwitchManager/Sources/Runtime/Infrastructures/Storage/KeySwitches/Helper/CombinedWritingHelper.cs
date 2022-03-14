using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper
{
    public static class CombinedWritingHelper
    {
        public static void Write( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, KeySwitch prefix, string suffix, IObserver<string>? loggingSubject, Func<Stream, IKeySwitchWriter> writerFactory )
        {
            var filePath = CreatePathHelper.CreateFilePath( prefix.DeveloperName, prefix.ProductName, string.Empty, suffix, outputDirectory );

            // Some writers require Read/Write access stream
            using var stream = filePath.OpenStream( FileMode.Create, FileAccess.ReadWrite );
            using var fileWriter = writerFactory.Invoke( stream );

            fileWriter.Write( keySwitches, loggingSubject );
            loggingSubject?.OnNext( $"{nameof(CombinedWritingHelper)} : {keySwitches.Count} records has been written" );
        }
    }
}
