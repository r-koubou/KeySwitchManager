using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class SingleExportStrategy : IExportStrategy
    {
        private IExportContentWriter ContentWriter { get; }
        private IExportContentFactory ContentFactory { get; }

        public SingleExportStrategy( IExportContentWriter contentWriter, IExportContentFactory contentFactory )
        {
            ContentWriter  = contentWriter;
            ContentFactory = contentFactory;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            var content = await ContentFactory.CreateAsync( keySwitches );
            await ContentWriter.WriteAsync( content );
        }
    }
}
