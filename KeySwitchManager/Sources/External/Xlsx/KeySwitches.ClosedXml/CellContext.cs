using ClosedXML.Excel;

namespace KeySwitchManager.Xlsx.KeySwitches.ClosedXml
{
    public struct CellContext
    {
        public int RowIndex;
        public IXLWorksheet Sheet;
        public IXLRow Row;
    }
}