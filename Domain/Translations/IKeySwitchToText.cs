using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.KeySwitches.Aggregate;

namespace ArticulationManager.Domain.Translations
{
    public interface IKeySwitchToText : IDataTranslator<KeySwitch, IText>
    {}
}