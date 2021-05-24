using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators.Helpers;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Helpers;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Translators
{
    internal class MidiNoteOnMessageTranslator : IMidiMessageTranslator
    {
        private int StartColumn { get; }

        public MidiNoteOnMessageTranslator( int startColumn )
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
                // channel
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage(
                    type,
                    TranslateMidiMessageType.ChannelInStatus,
                    ( message.Status.Value & 0xF ) + 1, // zero-based index to one-based index
                    sheet,
                    row,
                    column
                );
                // note
                column = KeySwitchToClosedXmlModelHelper.UpdateCellFromMidiMessage(
                    type,
                    TranslateMidiMessageType.Data1,
                    message.DataByte1.Value,
                    sheet,
                    row,
                    column,
                    ( cell, value ) =>
                    {
                        var noteName = MidiNoteNameHelper.GetNoteNameList()[ value ];
                        cell.Value = noteName;
                    }
                );
                // velocity
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