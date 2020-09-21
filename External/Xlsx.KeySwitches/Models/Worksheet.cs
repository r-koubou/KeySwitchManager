using System;
using System.Collections.Generic;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class Worksheet
    {
        public string Name { get; }
        public OutputNameCell OutputNameCell { get; set; } = OutputNameCell.Empty;

        public readonly List<Row> Rows = new List<Row>();

        public Worksheet( string name )
        {
            Name = name ?? throw new ArgumentNullException( nameof( name ) );
        }
    }
}