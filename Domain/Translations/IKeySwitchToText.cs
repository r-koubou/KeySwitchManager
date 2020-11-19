using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;

namespace KeySwitchManager.Domain.Translations
{
    public interface IKeySwitchToText : IDataTranslator<KeySwitch, IText>
    {}
}