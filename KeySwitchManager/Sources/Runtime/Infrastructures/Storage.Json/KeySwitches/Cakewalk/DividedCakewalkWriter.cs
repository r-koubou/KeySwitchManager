using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk
{
    public class DividedCakewalkWriter : IKeySwitchWriter
    {
        private const string Suffix = ".artmap";

        public DirectoryPath OutputDirectory { get; }
        private Encoding FileEncoding { get; }
        public bool LeaveOpen => false;

        public DividedCakewalkWriter( DirectoryPath outputDirectory ) : this( outputDirectory, Encoding.UTF8 ) {}

        public DividedCakewalkWriter( DirectoryPath outputDirectory, Encoding filEncoding )
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
                stream => new CakewalkWriter( stream, FileEncoding, true )
            );
        }
    }
}
