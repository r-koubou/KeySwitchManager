using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper
{
    public static class MultipleWritingHelper
    {
        public static void Write( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, string suffix, IObserver<string>? loggingSubject, Func<Stream, IKeySwitchWriter> writerFactory )
            => WriteAsync( keySwitches, outputDirectory, suffix, loggingSubject, writerFactory ).GetAwaiter().GetResult();

        public static async Task WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, string suffix, IObserver<string>? loggingSubject, Func<Stream, IKeySwitchWriter> writerFactory )
        {
            foreach( var x in keySwitches )
            {
                var filePath = CreatePathHelper.CreateFilePath( x, suffix, outputDirectory );

                // Some writers require Read/Write access stream
                await using var stream = filePath.OpenStream( FileMode.Create, FileAccess.ReadWrite );
                using var fileWriter = writerFactory.Invoke( stream );

                await fileWriter.WriteAsync( new[] { x }, loggingSubject );
            }

            loggingSubject?.OnNext( $"{nameof(MultipleWritingHelper)} : {keySwitches.Count} records has been written" );
        }

        public static void Write( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, string suffix, IObserver<string>? logging, Func<Stream, KeySwitch, IObserver<string>?, Task> write )
            => WriteAsync( keySwitches, outputDirectory, suffix, logging, write ).GetAwaiter().GetResult();

        public static async Task WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, DirectoryPath outputDirectory, string suffix, IObserver<string>? logging, Func<Stream, KeySwitch, IObserver<string>?, Task> write )
        {
            foreach( var x in keySwitches )
            {
                logging?.OnNext( $"{x.DeveloperName} | {x.ProductName} | {x.InstrumentName}" );

                var filePath = CreatePathHelper.CreateFilePath( x, suffix, outputDirectory );

                // Some writers require Read/Write access stream
                await using var stream = filePath.OpenStream( FileMode.Create, FileAccess.ReadWrite );
                write.Invoke( stream, x, logging );
            }

            logging?.OnNext( $"{nameof(MultipleWritingHelper)} : {keySwitches.Count} records has been written" );
        }

    }
}
