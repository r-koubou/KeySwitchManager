using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.KeySwitch.Translations
{
    public interface IKeySwitchListToJsonListText : IDataTranslator<IReadOnlyCollection<Domain.KeySwitches.KeySwitch>, IText>
    {}
}