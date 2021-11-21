using System;
using System.Collections.Generic;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne
{
    public class MultipleStudioOneWriter : IKeySwitchWriter
    {
        private const string Suffix = ".keyswitch";

        public DirectoryPath OutputDirectory { get; }
        private Encoding FileEncoding { get; }
        public bool LeaveOpen => false;

        public MultipleStudioOneWriter( DirectoryPath outputDirectory ) : this( outputDirectory, Encoding.UTF8 ) {}

        public MultipleStudioOneWriter( DirectoryPath outputDirectory, Encoding filEncoding )
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
                stream => new StudioOneWriter( stream, FileEncoding, true )
            );
        }
    }
}
