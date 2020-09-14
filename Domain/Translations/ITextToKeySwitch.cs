using System.Collections.Generic;

using ArticulationManager.Domain.Articulations.Aggregate;
using ArticulationManager.Domain.Commons;

namespace ArticulationManager.Domain.Translations
{
    public interface ITextToKeySwitch : IDataTranslator<IText, KeySwitch>
    {}
}