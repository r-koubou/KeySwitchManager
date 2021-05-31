using System.Collections.Generic;

using KeySwitchManager.Storage.Yaml.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models
{
    public class YamlModel
    {
        public IList<KeySwitchModel> KeySwitches { get; set; } = new List<KeySwitchModel>();
    }
}
