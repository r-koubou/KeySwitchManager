using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.Translators;

namespace KeySwitchManager.UseCases.KeySwitches.StudioOne.Translators
{
    public interface IKeySwitchToStudioOneKeySwitchModel
        : IDataTranslator<KeySwitch, IText>
    {}
}