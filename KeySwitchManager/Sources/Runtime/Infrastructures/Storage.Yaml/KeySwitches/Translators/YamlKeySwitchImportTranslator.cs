using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators.Helpers;

using YamlDotNet.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Translators
{
    public class YamlKeySwitchImportTranslator : IDataTranslator<IText, IReadOnlyCollection<KeySwitch>>
    {
        public IReadOnlyCollection<KeySwitch> Translate( IText source )
        {
            var result = new List<KeySwitch>();
            var deserializer = new DeserializerBuilder().Build();
            var model = deserializer.Deserialize<YamlModel>( source.Value );

            foreach( var i in model.KeySwitches )
            {
                var x = YamlModelToKeySwitchHelper.Translate( i );
                result.Add( x );
            }

            return result;
        }
    }
}