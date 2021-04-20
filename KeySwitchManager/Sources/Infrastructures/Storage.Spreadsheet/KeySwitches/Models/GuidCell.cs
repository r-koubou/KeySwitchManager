using System;

using RkHelper.Text;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models
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