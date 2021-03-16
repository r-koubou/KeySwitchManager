using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.StudioOneKeySwitch.Translations
{
    public interface IKeySwitchToStudioOneKeySwitchModel
        : IDataTranslator<KeySwitch, IText>
    {}
}