using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Claunia.PropertyList;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Translators;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic
{
    public class LogicExportContentFactory : IExportContentFactory
    {
        public Task<IContent> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken = default )
        {
            KeySwitchValidateHelper.ValidateOneElement( keySwitches );

            var source = keySwitches.First();
            var rootObject = new LogicExportTranslator().Translate( source );
            var memoryStream = new MemoryStream();

            PropertyListParser.SaveAsXml( rootObject, memoryStream );

            return Task.FromResult<IContent>( new BinaryContent( memoryStream.ToArray() ) );
        }
    }
}
