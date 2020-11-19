using System.Data;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public struct CellContext
    {
        public int RowIndex;
        public DataTable Sheet;
        public DataRow Row;
    }
}