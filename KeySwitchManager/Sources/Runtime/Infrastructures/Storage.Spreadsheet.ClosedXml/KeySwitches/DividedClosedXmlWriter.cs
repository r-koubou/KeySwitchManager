using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class DividedClosedXmlWriter : IKeySwitchWriter
    {
        private const string Suffix = ".xlsx";

        public DirectoryPath OutputDirectory { get; }
        public bool LeaveOpen => false;

        public DividedClosedXmlWriter( DirectoryPath outputDirectory )
        {
            OutputDirectory = outputDirectory;
        }

        public void Dispose() {}

        async Task IKeySwitchWriter.WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject )
        {
            await MultipleWritingHelper.WriteAsync(
                keySwitches,
                OutputDirectory,
                Suffix,
                loggingSubject,
                stream => new ClosedXmlWriter( stream, true )
            );
        }
    }
}
