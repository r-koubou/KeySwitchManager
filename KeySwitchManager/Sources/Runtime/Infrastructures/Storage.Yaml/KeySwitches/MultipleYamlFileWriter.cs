using System;
using System.Collections.Generic;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public class MultipleYamlFileWriter : IKeySwitchWriter
    {
        private const string Suffix = ".yaml";

        public DirectoryPath OutputDirectory { get; }
        private Encoding FileEncoding { get; }
        public bool LeaveOpen => false;

        public MultipleYamlFileWriter( DirectoryPath outputDirectory ) : this( outputDirectory, Encoding.UTF8 ) {}

        public MultipleYamlFileWriter( DirectoryPath outputDirectory, Encoding filEncoding )
        {
            OutputDirectory = outputDirectory;
            FileEncoding    = filEncoding;
        }

        public void Dispose() {}

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject = null )
        {
            MultipleWritingHelper.Write(
                keySwitches,
                OutputDirectory,
                Suffix,
                loggingSubject,
                stream => new YamlKeySwitchWriter( stream, FileEncoding, true )
            );
        }
    }
}
