using System.Collections.Generic;
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

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            var groupByDeveloperAndProduct = KeySwitchHelper.GroupBy( keySwitches );

            foreach( var kvp in groupByDeveloperAndProduct )
            {
                var developer = kvp.Key.Item1;
                var product = kvp.Key.Item2;
                var items = kvp.Value;

                var content = await ContentFactory.CreateAsync( items );
                var contentWriter = await ContentWriterFactory.CreateAsync( items );

                await contentWriter.WriteAsync( content );
            }
        }
    }
}
