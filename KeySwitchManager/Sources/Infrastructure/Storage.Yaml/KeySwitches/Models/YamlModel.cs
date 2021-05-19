using System.Collections.Generic;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models
{
    public class YamlModel
    {
        public IList<KeySwitchModel> KeySwitches { get; set; } = new List<KeySwitchModel>();
    }
}
