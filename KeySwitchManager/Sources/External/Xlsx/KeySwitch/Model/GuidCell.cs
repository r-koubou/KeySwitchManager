using System;

using RkHelper.Text;

namespace KeySwitchManager.Xlsx.KeySwitch.Model
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
            Value = StringHelper.IsEmpty( name ) ? string.Empty : name;
        }
        public GuidCell( Guid guid )
        {
            Value = guid.ToString("D");
        }

    }
}