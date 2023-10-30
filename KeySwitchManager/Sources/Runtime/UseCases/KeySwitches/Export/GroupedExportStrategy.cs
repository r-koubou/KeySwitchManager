using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class GroupedExportStrategy : IExportStrategy
    {
        private IExportContentWriterFactory ContentWriterFactory { get; }
        private IExportContentFactory ContentFactory { get; }

        public GroupedExportStrategy( IExportContentWriterFactory contentWriterFactory, IExportContentFactory contentFactory )
        {
            ContentWriterFactory = contentWriterFactory;
            ContentFactory       = contentFactory;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken )
        {
            var groupByDeveloperAndProduct = KeySwitchHelper.GroupBy( keySwitches );

            foreach( var kvp in groupByDeveloperAndProduct )
            {
                if( cancellationToken.IsCancellationRequested )
                {
                    break;
                }

                var items = kvp.Value;

                var content = await ContentFactory.CreateAsync( items, cancellationToken );
                if( cancellationToken.IsCancellationRequested )
                {
                    return;
                }

                var contentWriter = await ContentWriterFactory.CreateAsync( items, cancellationToken );
                if( cancellationToken.IsCancellationRequested )
                {
                    return;
                }

                await contentWriter.WriteAsync( content, cancellationToken );
            }
        }
    }
}
