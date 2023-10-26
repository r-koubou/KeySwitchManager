using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public class MultipleFilesOutputStrategy : IExportStrategy
    {
        private IExportContentFactory ContentFactory { get; }

        public MultipleFilesOutputStrategy( IExportContentFactory contentFactory )
        {
            ContentFactory = contentFactory;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, IContentWriter contentWriter )
        {
            foreach( var keySwitch in keySwitches )
            {
                var content = ContentFactory.Create( keySwitches );
                await contentWriter.WriteAsync( content );
            }
        }
    }
}
