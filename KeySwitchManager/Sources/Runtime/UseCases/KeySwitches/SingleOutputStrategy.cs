using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public class SingleOutputStrategy : IExportStrategy
    {
        private IContentDataTranslator DataTranslator { get; }

        public SingleOutputStrategy( IContentDataTranslator dataTranslator )
        {
            DataTranslator = dataTranslator;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, IContentWriter contentWriter )
        {
            var content = DataTranslator.Translate( keySwitches );
            await contentWriter.WriteAsync( content );
        }
    }
}
