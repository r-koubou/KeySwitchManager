using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase
{
    [Obsolete( "Use " + nameof(CubaseExportContentFactory) + " instead" )]
    public sealed class CubaseWriter : IKeySwitchWriter
    {
        private const string Suffix = ".expressionmap";

        public DirectoryPath OutputDirectory { get; }
        private Encoding FileEncoding { get; }
        public bool LeaveOpen => false;

        public CubaseWriter( DirectoryPath outputDirectory ) : this( outputDirectory, Encoding.UTF8 ) {}

        public CubaseWriter( DirectoryPath outputDirectory, Encoding filEncoding )
        {
            OutputDirectory = outputDirectory;
            FileEncoding    = filEncoding;
        }

        public void Dispose() {}

        async Task IKeySwitchWriter.WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? logging )
            => await MultipleWritingHelper.WriteAsync( keySwitches, OutputDirectory, Suffix, logging, WriteImplAsync );

        private async Task WriteImplAsync( Stream stream, KeySwitch keySwitch, IObserver<string>? logging )
        {
            await using var writer = new StreamWriter( stream, FileEncoding, IKeySwitchWriter.DefaultStreamWriterBufferSize, LeaveOpen );
            var xmlText = new CubaseExportTranslator().Translate( keySwitch );

            await writer.WriteLineAsync( xmlText.Value );

        }
    }
}
