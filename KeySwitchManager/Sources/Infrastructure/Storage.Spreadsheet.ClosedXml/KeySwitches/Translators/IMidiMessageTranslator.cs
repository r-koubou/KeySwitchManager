using System;
using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators
{
    [Flags]
    internal enum TranslateMidiMessageType
    {
        Status = 0x1,
        ChannelInStatus = 0x2,
        Data1 = 0x4,
        Data2 = 0x8,
    }

    internal class MidiMessageCellInfo
    {
        public string HeaderCellName { get; }
        public int MidiDataValue { get; }

        public MidiMessageCellInfo( string headerCellName, int midiDataValue )
        {
            HeaderCellName = headerCellName;
            MidiDataValue  = midiDataValue;
        }
    }

    internal interface IMidiMessageTranslator
    {
        int Translate(
            IEnumerable<IMidiMessage> midiMessages,
            IXLWorksheet sheet,
            int headerRow,
            int row,
            TranslateMidiMessageType type );
    }
}