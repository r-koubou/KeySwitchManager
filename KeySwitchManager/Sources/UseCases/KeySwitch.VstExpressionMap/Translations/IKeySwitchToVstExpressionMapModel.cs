using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.Translations;

namespace KeySwitchManager.UseCases.KeySwitch.VstExpressionMap.Translations
{
    public interface IKeySwitchToVstExpressionMapModel
        : IDataTranslator<Domain.KeySwitches.KeySwitch, IText>
    {}
}