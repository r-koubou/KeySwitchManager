using System.Collections.Generic;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models
{
    public class Workbook
    {
        public readonly List<Worksheet> Worksheets = new List<Worksheet>();
    }
}