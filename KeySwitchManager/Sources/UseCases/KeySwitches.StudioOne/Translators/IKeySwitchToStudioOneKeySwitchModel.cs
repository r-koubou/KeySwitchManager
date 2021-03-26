using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.KeySwitches.StudioOne.Translators
{
    public interface IKeySwitchToStudioOneKeySwitchModel
        : IDataTranslator<KeySwitch, IText>
    {}
}