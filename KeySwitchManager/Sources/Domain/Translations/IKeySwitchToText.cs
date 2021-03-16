using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;

namespace KeySwitchManager.Domain.Translations
{
    public interface IKeySwitchToText : IDataTranslator<KeySwitch, IText>
    {}
}