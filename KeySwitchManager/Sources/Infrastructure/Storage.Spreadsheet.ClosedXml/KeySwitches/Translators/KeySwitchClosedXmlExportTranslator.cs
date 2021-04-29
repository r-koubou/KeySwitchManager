using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators.Helpers;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators
{
    public class KeySwitchClosedXmlExportTranslator : IDataTranslator<IReadOnlyCollection<KeySwitch>, XLWorkbook>
    {
        private XLWorkbook Template { get; }

        public KeySwitchClosedXmlExportTranslator( XLWorkbook template )
        {
            Template = template;
        }

        public XLWorkbook Translate( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            return KeySwitchToClosedXmlModelHelper.Translate( keySwitches, Template );
        }
    }
}