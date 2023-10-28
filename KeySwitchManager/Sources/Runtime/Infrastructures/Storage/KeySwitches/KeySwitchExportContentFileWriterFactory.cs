using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class KeySwitchExportContentFileWriterFactory : IExportContentWriterFactory
    {
        private string Suffix { get; }
        private DirectoryPath OutputDirectory { get; }
        private Func<IReadOnlyCollection<KeySwitch>, KeySwitch> BaseNameSelector { get; }

        public KeySwitchExportContentFileWriterFactory( string suffix, DirectoryPath outputDirectory )
          : this( suffix, outputDirectory, (x) => x.First() ) {}

        public KeySwitchExportContentFileWriterFactory( string suffix, DirectoryPath outputDirectory, Func<IReadOnlyCollection<KeySwitch>, KeySwitch> baseNameSelector )
        {
            Suffix           = suffix;
            OutputDirectory  = outputDirectory;
            BaseNameSelector = baseNameSelector;
        }

        public Task<IExportContentWriter> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            var outputPath = CreatePathHelper.CreateFilePath( BaseNameSelector( keySwitches ), Suffix, OutputDirectory );
            OutputDirectory.CreateNew();

            return Task.FromResult<IExportContentWriter>( new FileExportContentWriter( outputPath ) );
        }
    }
}
