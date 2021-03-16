using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.KeySwitch.StudioOne.Translations
{
    public interface IKeySwitchToStudioOneKeySwitchModel
        : IDataTranslator<Domain.KeySwitches.KeySwitch, IText>
    {}
}