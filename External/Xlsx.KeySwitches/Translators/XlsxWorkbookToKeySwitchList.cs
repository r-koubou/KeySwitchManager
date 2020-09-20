using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.UseCases.KeySwitches.Translations;

namespace KeySwitchManager.Xlsx.KeySwitches.Translators
{
    public class XlsxWorkbookToKeySwitchList : IXlsxWorkbookToKeySwitchList
    {
        public IEnumerable<KeySwitch> Translate( FilePath source )
        {
            throw new NotImplementedException();
        }
    }
}