using ClosedXML.Excel;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper
{
    internal struct CellContext
    {
        public int RowIndex;
        public IXLWorksheet Sheet;
        public IXLRow Row;
    }
}