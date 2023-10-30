using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class SingleExportStrategy : IExportStrategy
    {
        private IExportContentWriterFactory ContentWriterFactory { get; }
        private IExportContentFactory ContentFactory { get; }

        public SingleExportStrategy( IExportContentWriterFactory contentWriterFactory, IExportContentFactory contentFactory )
        {
            ContentWriterFactory = contentWriterFactory;
            ContentFactory       = contentFactory;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken )
        {
            var contentWriter = await ContentWriterFactory.CreateAsync( keySwitches );
            var content = await ContentFactory.CreateAsync( keySwitches );
            await contentWriter.WriteAsync( content );
        }
    }
}
