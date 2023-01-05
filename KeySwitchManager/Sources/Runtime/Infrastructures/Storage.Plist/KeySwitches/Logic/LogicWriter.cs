using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Claunia.PropertyList;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Translators;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic
{
    public sealed class LogicWriter : IKeySwitchWriter
    {
        private const string Suffix = ".plist";

        public DirectoryPath OutputDirectory { get; }
        private Encoding FileEncoding { get; }
        public bool LeaveOpen => false;

        public LogicWriter( DirectoryPath outputDirectory ) : this( outputDirectory, Encoding.UTF8 ) {}

        public LogicWriter( DirectoryPath outputDirectory, Encoding filEncoding )
        {
            OutputDirectory = outputDirectory;
            FileEncoding    = filEncoding;
        }

        public void Dispose() {}

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? logging = null )
            => MultipleWritingHelper.Write( keySwitches, OutputDirectory, Suffix, logging, WriteImpl );

        private void WriteImpl( Stream stream, KeySwitch keySwitch, IObserver<string>? logging )
        {
            var rootObject = new LogicExportTranslator().Translate( keySwitch );
            PropertyListParser.SaveAsXml( rootObject, stream );
        }
    }
}
