using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.Translators;

namespace KeySwitchManager.UseCases.KeySwitches.VstExpressionMap.Translators
{
    public interface IKeySwitchToVstExpressionMapModel
        : IDataTranslator<KeySwitch, IText>
    {}
}