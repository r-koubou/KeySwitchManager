using System.Collections.Generic;

using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models
{
    public class Workbook
    {
        public readonly List<Worksheet> Worksheets = new List<Worksheet>();
    }
}