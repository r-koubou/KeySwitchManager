using ClosedXML.Excel;

namespace KeySwitchManager.Xlsx.KeySwitch.ClosedXml
{
    public struct CellContext
    {
        public int RowIndex;
        public IXLWorksheet Sheet;
        public IXLRow Row;
    }
}