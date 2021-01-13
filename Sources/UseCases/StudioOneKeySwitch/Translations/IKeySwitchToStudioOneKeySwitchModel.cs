using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.StudioOneKeySwitch.Translations
{
    public interface IKeySwitchToStudioOneKeySwitchModel
        : IDataTranslator<KeySwitch, IText>
    {}
}