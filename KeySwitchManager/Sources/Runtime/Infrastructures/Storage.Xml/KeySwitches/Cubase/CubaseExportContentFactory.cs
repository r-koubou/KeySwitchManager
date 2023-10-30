using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase.Translators;
using KeySwitchManager.UseCase.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase
{
    public class CubaseExportContentFactory : IExportContentFactory
    {
        public Task<IContent> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken = default )
        {
            KeySwitchValidateHelper.ValidateOneElement( keySwitches );

            var source = keySwitches.First();
            var xmlText = new CubaseExportTranslator().Translate( source );

            return Task.FromResult<IContent>( new StringContent( xmlText.Value ) );
        }
    }
}
