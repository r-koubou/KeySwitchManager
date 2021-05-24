using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators.Helpers;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators
{
    internal class MidiMessageTranslator : IMidiMessageTranslator
    {
        private int StartColumn { get; }

        public MidiMessageTranslator( int startColumn )
        {
            StartColumn     = startColumn;
        }

        public int Translate(
            IEnumerable<IMidiMessage> midiMessages,
            IXLWorksheet sheet,
            int headerRow,
            int row,
            TranslateMidiMessageType type )
        {
            var column = StartColumn;

            foreach( var message in midiMessages )
            {
                // Status byte / Channel Number
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage( type, TranslateMidiMessageType.Status, message.Status.Value, sheet, row, column );
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage( type, TranslateMidiMessageType.ChannelInStatus, message.Status.Value & 0xF, sheet, row, column );
                // Data byte
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage( type, TranslateMidiMessageType.Data1, message.DataByte1.Value, sheet, row, column );
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage( type, TranslateMidiMessageType.Data2, message.DataByte2.Value, sheet, row, column );
            }

            return column;
        }
    }
}