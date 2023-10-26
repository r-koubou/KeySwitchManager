using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public class MultipleFilesOutputStrategy : IExportStrategy
    {
        private IContentDataTranslator DataTranslator { get; }

        public MultipleFilesOutputStrategy( IContentDataTranslator dataTranslator )
        {
            DataTranslator = dataTranslator;
        }

        async Task IExportStrategy.ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, IContentWriter contentWriter )
        {
            foreach( var keySwitch in keySwitches )
            {
                var content = DataTranslator.Translate( keySwitches );
                await contentWriter.WriteAsync( content );
            }
        }
    }
}
