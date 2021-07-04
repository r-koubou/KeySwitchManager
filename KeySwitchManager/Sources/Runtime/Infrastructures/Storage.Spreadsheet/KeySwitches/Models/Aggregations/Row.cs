using System.Collections.Generic;

using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Helpers;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Aggregations
{
    public class Row
    {
        public class MidiNote
        {
            public MidiChannelCell Channel { get; set; }
                = new MidiChannelCell( MidiChannelCell.MinValue );
            public MidiNoteNumberCell Note { get; set; }
                = new MidiNoteNumberCell( MidiNoteNameHelper.GetNoteNameList()[ 0 ] );
            public MidiNoteVelocityCell Velocity { get; set; }
                = new MidiNoteVelocityCell( MidiNoteVelocityCell.MinValue );
        }

        public class MidiControlChange
        {
            public MidiChannelCell Channel { get; set; }
                = new MidiChannelCell( MidiChannelCell.MinValue );
            public MidiControlChangeNumberCell CcNumber { get; set; }
                = new MidiControlChangeNumberCell( MidiControlChangeNumberCell.MinValue );
            public MidiControlChangeValueCell CcValue { get; set; }
                = new MidiControlChangeValueCell( MidiControlChangeValueCell.MinValue );
        }

        public class MidiProgramChange
        {
            public MidiChannelCell Channel { get; set; }
                = new MidiChannelCell( MidiChannelCell.MinValue );
            public MidiProgramChangeCell Data { get; set; }
                = new MidiProgramChangeCell( MidiProgramChangeCell.MinValue );
        }

        public ArticulationNameCell ArticulationName { get; }
        public List<MidiNote> MidiNoteList { get; } = new List<MidiNote>();
        public List<MidiControlChange> MidiControlChangeList { get; } = new List<MidiControlChange>();
        public List<MidiProgramChange> MidiProgramChangeList { get; } = new List<MidiProgramChange>();

        public Dictionary<string, ExtraDataCell> Extra { get; } = new Dictionary<string, ExtraDataCell>();

        public Row( ArticulationNameCell name )
        {
            ArticulationName = name;
        }
    }
}