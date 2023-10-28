using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class MultipleExportStrategy : IExportStrategy
    {
        private IExportContentWriterFactory<KeySwitch> ContentWriterFactory { get; }
        private IExportContentFactory ContentFactory { get; }

        public MultipleExportStrategy( IExportContentWriterFactory<KeySwitch> contentWriterFactory, IExportContentFactory contentFactory )
        {
            ContentWriterFactory = contentWriterFactory;
            ContentFactory       = contentFactory;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            foreach( var x in keySwitches )
            {
                var content = await ContentFactory.CreateAsync( new[] { x } );
                var contentWriter = ContentWriterFactory.Create( x );
                await contentWriter.WriteAsync( content );
            }
        }
    }
}
