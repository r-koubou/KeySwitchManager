using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.KeySwitches.Aggregate;

namespace ArticulationManager.Domain.Translations
{
    public interface ITextToKeySwitch : IDataTranslator<IText, KeySwitch>
    {}
}