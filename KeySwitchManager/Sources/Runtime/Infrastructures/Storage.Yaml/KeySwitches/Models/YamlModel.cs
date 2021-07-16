using System.Collections.Generic;

using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models
{
    public class YamlModel
    {
        public IList<KeySwitchModel> KeySwitches { get; set; } = new List<KeySwitchModel>();
    }
}
