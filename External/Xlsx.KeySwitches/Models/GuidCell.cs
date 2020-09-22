using KeySwitchManager.Common.Utilities;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class GuidCell
    {
        public static readonly GuidCell Empty = new GuidCell();

        public string Value { get; }

        private GuidCell()
        {
            Value = CellConstants.EmptyCellValue;
        }

        public GuidCell( string name )
        {
            if( StringHelper.IsNullOrTrimEmpty( name ) )
            {
                Value = string.Empty;
            }

            Value = name;
        }
    }
}