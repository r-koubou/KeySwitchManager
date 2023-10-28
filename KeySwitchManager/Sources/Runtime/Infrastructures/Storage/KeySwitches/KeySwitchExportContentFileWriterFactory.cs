using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class KeySwitchExportContentFileWriterFactory : IExportContentWriterFactory
    {
        private DirectoryPath OutputDirectory { get; }
        private IExportPathBuilder PathBuilder { get; }

        public KeySwitchExportContentFileWriterFactory( string suffix, DirectoryPath outputDirectory )
          : this(
              outputDirectory,
              new DefaultExportPathBuilder( suffix, outputDirectory ) ) {}

        public KeySwitchExportContentFileWriterFactory( DirectoryPath outputDirectory, IExportPathBuilder pathBuilder )
        {
            OutputDirectory = outputDirectory;
            PathBuilder     = pathBuilder;
        }

        public Task<IExportContentWriter> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            var outputPath = PathBuilder.Build( keySwitches );
            OutputDirectory.CreateNew();

            return Task.FromResult<IExportContentWriter>( new FileExportContentWriter( outputPath ) );
        }
    }
}
