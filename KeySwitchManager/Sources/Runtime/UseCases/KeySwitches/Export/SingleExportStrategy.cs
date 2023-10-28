using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class SingleExportStrategy : IExportStrategy
    {
        async Task IExportStrategy.ExportAsync(
            IReadOnlyCollection<KeySwitch> keySwitches,
            IExportContentWriter contentWriter,
            IExportContentFactory contentFactory )
        {
            var content = await contentFactory.CreateAsync( keySwitches );
            await contentWriter.WriteAsync( content );
        }
    }
}
