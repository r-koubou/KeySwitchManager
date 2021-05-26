using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.MidiMessages.Models.Entities;
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
                // Status byte
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage(
                    type,
                    TranslateMidiMessageType.Status,
                    message.Status.Value,
                    sheet,
                    row,
                    column
                );
                // Channel Number
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage(
                    type,
                    TranslateMidiMessageType.ChannelInStatus,
                    ( message.Status.Value & 0xF ) + 1, // Zero-based index to One-based index
                    sheet,
                    row,
                    column
                );
                // Data byte1
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage(
                    type,
                    TranslateMidiMessageType.Data1,
                    message.DataByte1.Value,
                    sheet,
                    row,
                    column
                );
                // Data byte2
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage(
                    type,
                    TranslateMidiMessageType.Data2,
                    message.DataByte2.Value,
                    sheet,
                    row,
                    column
                );
            }

            return column;
        }
    }
}