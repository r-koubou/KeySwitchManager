using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public class DividedYamlFileWriter : IKeySwitchWriter
    {
        private const string Suffix = ".yaml";

        public DirectoryPath OutputDirectory { get; }
        private Encoding FileEncoding { get; }
        public bool LeaveOpen => false;

        public DividedYamlFileWriter( DirectoryPath outputDirectory ) : this( outputDirectory, Encoding.UTF8 ) {}

        public DividedYamlFileWriter( DirectoryPath outputDirectory, Encoding filEncoding )
        {
            OutputDirectory = outputDirectory;
            FileEncoding    = filEncoding;
        }

        public void Dispose() {}

        async Task IKeySwitchWriter.WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject )
        {
            await MultipleWritingHelper.WriteAsync(
                keySwitches,
                OutputDirectory,
                Suffix,
                loggingSubject,
                stream => new YamlKeySwitchWriter( stream, FileEncoding, true )
            );
        }
    }
}
