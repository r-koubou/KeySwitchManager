using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.KeySwitch.Translations
{
    public interface IJsonListToKeySwitchList : IDataTranslator<IText, IReadOnlyCollection<Domain.KeySwitches.KeySwitch>>
    {}
}