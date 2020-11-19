using System.Collections.Generic;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class Row
    {
        public class MidiNote
        {
            public MidiNoteNumberCell Note { get; set; }
                = new MidiNoteNumberCell( MidiNoteNumberCell.GetNoteNameList()[ 0 ] );
            public MidiNoteVelocityCell Velocity { get; set; }
                = new MidiNoteVelocityCell( MidiNoteVelocityCell.MinValue );
        }

        public class MidiControlChange
        {
            public MidiControlChangeNumberCell CcNumber { get; set; }
                = new MidiControlChangeNumberCell( MidiControlChangeNumberCell.MinValue );
            public MidiControlChangeValueCell CcValue { get; set; }
                = new MidiControlChangeValueCell( MidiControlChangeValueCell.MinValue );
        }

        public class MidiProgramChange
        {
            public MidiProgramChangeCell Channel { get; set; }
                = new MidiProgramChangeCell( MidiProgramChangeCell.MinValue );
            public MidiProgramChangeCell Data { get; set; }
                = new MidiProgramChangeCell( MidiProgramChangeCell.MinValue );
        }

        public ArticulationNameCell ArticulationName { get; }
        public List<MidiNote> MidiNoteList { get; } = new List<MidiNote>();
        public List<MidiControlChange> MidiControlChangeList { get; } = new List<MidiControlChange>();
        public List<MidiProgramChange> MidiProgramChangeList { get; } = new List<MidiProgramChange>();

        public Dictionary<string, ExtraDataCell> Extra { get; set; } = new Dictionary<string, ExtraDataCell>();

        public Row( ArticulationNameCell name )
        {
            ArticulationName = name;
        }
    }
}