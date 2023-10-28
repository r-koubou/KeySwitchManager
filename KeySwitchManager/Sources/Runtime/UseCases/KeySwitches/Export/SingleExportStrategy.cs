using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class SingleExportStrategy : IExportStrategy
    {
        private IExportContentFactory ContentFactory { get; }

        public SingleExportStrategy( IExportContentFactory contentFactory )
        {
            ContentFactory = contentFactory;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, IExportContentWriter exportContentWriter )
        {
            var content = ContentFactory.Create( keySwitches );
            await exportContentWriter.WriteAsync( content );
        }
    }
}
