using System.Collections.Generic;

using KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class Workbook
    {
        public readonly List<Worksheet> Worksheets = new List<Worksheet>();
    }
}