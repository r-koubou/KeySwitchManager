using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.Translators;

namespace KeySwitchManager.UseCases.KeySwitches.Translators
{
    public interface IKeySwitchListToJsonListText : IDataTranslator<IReadOnlyCollection<KeySwitch>, IText>
    {}
}