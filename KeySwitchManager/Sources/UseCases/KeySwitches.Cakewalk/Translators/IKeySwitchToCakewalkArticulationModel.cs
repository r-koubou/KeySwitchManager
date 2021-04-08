using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.Translators;

namespace KeySwitchManager.UseCases.KeySwitches.Cakewalk.Translators
{
    public interface IKeySwitchToCakewalkArticulationModel
        : IDataTranslator<KeySwitch, IText>
    {}
}