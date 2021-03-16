using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;

namespace KeySwitchManager.Domain.Translations
{
    public interface ITextToKeySwitch : IDataTranslator<IText, KeySwitch>
    {}
}