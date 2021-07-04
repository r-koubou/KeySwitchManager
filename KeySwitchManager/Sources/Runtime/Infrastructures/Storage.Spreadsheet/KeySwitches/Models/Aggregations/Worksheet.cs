using System;
using System.Collections.Generic;

using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Aggregations
{
    public class Worksheet
    {
        public string Name { get; }
        public GuidCell GuidCell { get; set; } = GuidCell.Empty;
        public DeveloperNameCell DeveloperNameCell { get; set; } = DeveloperNameCell.Empty;
        public ProductNameCell ProductNameCell { get; set; } = ProductNameCell.Empty;
        public InstrumentNameCell InstrumentNameCell { get; set; } = InstrumentNameCell.Empty;
        public AuthorCell AuthorCell { get; set; } = AuthorCell.Empty;
        public DescriptionCell DescriptionCell { get; set; } = DescriptionCell.Empty;

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