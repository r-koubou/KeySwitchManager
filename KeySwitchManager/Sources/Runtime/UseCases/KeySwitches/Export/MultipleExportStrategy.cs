using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class MultipleExportStrategy : IExportStrategy
    {
        async Task IExportStrategy.ExportAsync(
            IReadOnlyCollection<KeySwitch> keySwitches,
            IExportContentWriter contentWriter,
            IExportContentFactory contentFactory )
        {
            foreach( var x in keySwitches )
            {
                var content = await contentFactory.CreateAsync( new[] { x } );
                await contentWriter.WriteAsync( content );
            }
        }
    }
}
