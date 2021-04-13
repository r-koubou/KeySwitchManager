using System.Collections.Generic;
using System.Linq;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators
{

    internal class ExtraDataTranslator
    {
        private KeySwitch KeySwitch { get; }
        private List<ExtraDataKey> HeaderTextList { get; }

        public ExtraDataTranslator( KeySwitch keySwitch )
        {
            KeySwitch = keySwitch;

            // Build Column header cell text by all extra keys
            var allExtKeys = keySwitch.Articulations.SelectMany( x => x.ExtraData.Keys ).Distinct();
            HeaderTextList = new List<ExtraDataKey>( allExtKeys );
        }

        public int Translate( IXLWorksheet sheet, int headerRow, int startRow, int startColumn )
        {
            var row = startRow;
            var column = startColumn;

            foreach( var articulation in KeySwitch.Articulations )
            {
                Translate( articulation, sheet, row, column );
                row++;
            }

            return column + HeaderTextList.Count;
        }

        private void Translate( Articulation articulation, IXLWorksheet sheet, int startRow, int startColumn )
        {
            var column = startColumn;
            var row = startRow;

            foreach( var extra in articulation.ExtraData )
            {
                var offset = HeaderTextList.IndexOf( extra.Key );
                sheet.Cell( row, column + offset ).Value = extra.Value;
            }
        }
    }
}