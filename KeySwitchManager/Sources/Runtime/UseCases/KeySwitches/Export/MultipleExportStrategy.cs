using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class MultipleExportStrategy : IExportStrategy
    {
        private IExportContentFactory ContentFactory { get; }

        public MultipleExportStrategy( IExportContentFactory contentFactory )
        {
            ContentFactory = contentFactory;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, IExportContentWriter exportContentWriter )
        {
            foreach( var keySwitch in keySwitches )
            {
                var content = ContentFactory.Create( keySwitches );
                await exportContentWriter.WriteAsync( content );
            }
        }
    }
}
