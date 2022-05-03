using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase
{
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

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? logging = null )
            => MultipleWritingHelper.Write( keySwitches, OutputDirectory, Suffix, logging, WriteImpl );

        private void WriteImpl( Stream stream, KeySwitch keySwitch, IObserver<string>? logging )
        {
            using var writer = new StreamWriter( stream, FileEncoding, IKeySwitchWriter.DefaultStreamWriterBufferSize, LeaveOpen );
            var xmlText = new CubaseExportTranslator().Translate( keySwitch );

            writer.WriteLine( xmlText );

        }
    }
}
