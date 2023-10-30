using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class MultipleExportStrategy : IExportStrategy
    {
        private IExportContentWriterFactory ContentWriterFactory { get; }
        private IExportContentFactory ContentFactory { get; }

        public MultipleExportStrategy( IExportContentWriterFactory contentWriterFactory, IExportContentFactory contentFactory )
        {
            ContentWriterFactory = contentWriterFactory;
            ContentFactory       = contentFactory;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            foreach( var x in keySwitches )
            {
                var source = new[] { x };
                var content = await ContentFactory.CreateAsync( source );
                var contentWriter = await ContentWriterFactory.CreateAsync( source );
                await contentWriter.WriteAsync( content );
            }
        }
    }
}
