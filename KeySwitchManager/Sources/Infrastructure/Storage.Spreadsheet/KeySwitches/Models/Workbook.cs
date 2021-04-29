using System.Collections.Generic;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class Workbook
    {
        public readonly List<Worksheet> Worksheets = new List<Worksheet>();
    }
}