using System;

using KeySwitchManager.Common.Text;

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
        public GuidCell( Guid guid )
        {
            Value = guid.ToString("D");
        }

    }
}