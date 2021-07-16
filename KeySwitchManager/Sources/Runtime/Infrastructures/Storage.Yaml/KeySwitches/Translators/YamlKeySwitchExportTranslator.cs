using System.Collections.Generic;
using System.IO;
using System.Text;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators.Helpers;

using YamlDotNet.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators
{
    public class YamlKeySwitchExportTranslator :
        IDataTranslator<IReadOnlyCollection<KeySwitch>, IText>
    {
        public IText Translate( IReadOnlyCollection<KeySwitch> source )
        {
            var yamlModel = new YamlModel();

            foreach( var i in source )
            {
                yamlModel.KeySwitches.Add( KeySwitchToYamlModelHelper.Translate( i ) );
            }

            var sb = new StringBuilder( 8192 );
            using var writer = new StringWriter( sb );

            var serializer = new Serializer();
            serializer.Serialize( writer, yamlModel );

            return new PlainText( sb.ToString() );
        }
    }
}