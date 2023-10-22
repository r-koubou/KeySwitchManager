using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class CombinedClosedXmlWriter : IKeySwitchWriter
    {
        private const string Suffix = ".xlsx";

        public DirectoryPath OutputDirectory { get; }
        public bool LeaveOpen => false;

        public CombinedClosedXmlWriter( DirectoryPath outputDirectory )
        {
            OutputDirectory = outputDirectory;
        }

        public void Dispose() {}

        async Task IKeySwitchWriter.WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject )
        {
            var groupByDeveloperAndProduct = KeySwitchHelper.GroupBy( keySwitches );

            foreach( var kvp in groupByDeveloperAndProduct )
            {
                var developer = kvp.Key.Item1;
                var product = kvp.Key.Item2;
                var items = kvp.Value;

                await CombinedWritingHelper.WriteAsync(
                    items,
                    OutputDirectory,
                    items.First(),
                    Suffix,
                    loggingSubject,
                    stream => new ClosedXmlWriter( stream, true )
                );
            }
        }
    }
}
