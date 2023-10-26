using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public class SingleOutputStrategy : IExportStrategy
    {
        private IExportContentFactory ContentFactory { get; }

        public SingleOutputStrategy( IExportContentFactory contentFactory )
        {
            ContentFactory = contentFactory;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, IContentWriter contentWriter )
        {
            var content = ContentFactory.Create( keySwitches );
            await contentWriter.WriteAsync( content );
        }
    }
}
