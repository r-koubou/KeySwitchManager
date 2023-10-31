using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Import
{
    public class YamlImportContentReader : IImportContentReader
    {
        public async Task<IReadOnlyCollection<KeySwitch>> ReadAsync( IContent content, CancellationToken cancellationToken = default )
        {
            await using var stream = await content.GetContentStreamAsync( cancellationToken );
            using var reader = new StreamReader( stream, Encoding.UTF8, true );

            var jsonText = await reader.ReadToEndAsync();

            return new YamlKeySwitchImportTranslator().Translate( new PlainText( jsonText ) );;
        }
    }
}
