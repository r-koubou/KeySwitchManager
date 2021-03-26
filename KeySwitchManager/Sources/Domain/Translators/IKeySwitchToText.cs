using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;

namespace KeySwitchManager.Domain.Translators
{
    public interface IKeySwitchToText : IDataTranslator<KeySwitch, IText>
    {}
}