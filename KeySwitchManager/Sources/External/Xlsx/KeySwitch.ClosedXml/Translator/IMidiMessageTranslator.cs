using System;
using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.MidiMessages.Entity;

namespace KeySwitchManager.Xlsx.KeySwitch.ClosedXml.Translator
{
    [Flags]
    internal enum TranslateMidiMessageType
    {
        Data1 = 0x1,
        Data2 = 0x2,
        Data3 = 0x4,
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
            string midiData1HeaderName,
            string midiData2HeaderName,
            string midiData3HeaderName,
            TranslateMidiMessageType type );
    }
}