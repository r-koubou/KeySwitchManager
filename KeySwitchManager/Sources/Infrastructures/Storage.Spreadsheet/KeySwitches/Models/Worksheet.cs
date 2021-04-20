using System;
using System.Collections.Generic;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models
{
    public class Worksheet
    {
        public string Name { get; }
        public OutputNameCell OutputNameCell { get; set; } = OutputNameCell.Empty;

        public GuidCell GuidCell { get; set; } = GuidCell.Empty;

        public readonly List<Row> Rows = new List<Row>();

        // TODO: Extra data that applies to the entire keyswitch.
        //       Spreadsheet version has not supported yet.
        public readonly Dictionary<string, ExtraDataCell> Extra = new Dictionary<string, ExtraDataCell>();

        public Worksheet( string name )
        {
            Name = name ?? throw new ArgumentNullException( nameof( name ) );
        }
    }
}