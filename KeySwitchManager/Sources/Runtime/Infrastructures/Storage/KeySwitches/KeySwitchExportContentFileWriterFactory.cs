using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class KeySwitchExportContentFileWriterFactory : IExportContentWriterFactory
    {
        private IDirectoryPath OutputDirectory { get; }
        private IExportPathBuilder PathBuilder { get; }

        protected KeySwitchExportContentFileWriterFactory( string suffix, IDirectoryPath outputDirectory )
          : this( new DefaultExportPathBuilder( suffix, outputDirectory ) ) {}

        protected KeySwitchExportContentFileWriterFactory( IExportPathBuilder pathBuilder )
        {
            OutputDirectory = pathBuilder.OutputDirectory;
            PathBuilder     = pathBuilder;
        }

        public virtual Task<IExportContentWriter> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken _ )
        {
            var outputPath = PathBuilder.Build( keySwitches );
            OutputDirectory.CreateNew();

            return Task.FromResult<IExportContentWriter>( new FileExportContentWriter( outputPath ) );
        }
    }
}
