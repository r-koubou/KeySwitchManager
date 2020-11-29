using System.Data;

using ClosedXML.Excel;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public struct CellContext
    {
        public int RowIndex;
        public IXLWorksheet Sheet;
        public IXLRow Row;
    }
}