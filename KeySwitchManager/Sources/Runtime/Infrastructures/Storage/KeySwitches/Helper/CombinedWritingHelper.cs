using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper
{
    public static class CombinedWritingHelper
    {
        public static void Write( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, KeySwitch prefix, string suffix, IObserver<string>? loggingSubject, Func<Stream, IKeySwitchWriter> writerFactory )
            => WriteAsync( keySwitches, outputDirectory, prefix, suffix, loggingSubject, writerFactory ).GetAwaiter().GetResult();

        public static async Task WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, KeySwitch prefix, string suffix, IObserver<string>? loggingSubject, Func<Stream, IKeySwitchWriter> writerFactory )
        {
            var filePath = CreatePathHelper.CreateFilePath( prefix.DeveloperName, prefix.ProductName, string.Empty, suffix, outputDirectory );

            // Some writers require Read/Write access stream
            await using var stream = filePath.OpenStream( FileMode.Create, FileAccess.ReadWrite );
            using var fileWriter = writerFactory.Invoke( stream );

            await fileWriter.WriteAsync( keySwitches, loggingSubject );
            loggingSubject?.OnNext( $"{nameof(CombinedWritingHelper)} : {keySwitches.Count} records has been written" );
        }
    }
}
