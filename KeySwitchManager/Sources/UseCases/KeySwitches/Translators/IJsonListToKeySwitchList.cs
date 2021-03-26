using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.KeySwitches.Translators
{
    public interface IJsonListToKeySwitchList : IDataTranslator<IText, IReadOnlyCollection<KeySwitch>>
    {}
}