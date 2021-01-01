using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Xlsx.KeySwitches.Helpers;

namespace KeySwitchManager.Xlsx.KeySwitches.ClosedXml.Translators
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
            string midiData1HeaderName,
            string midiData2HeaderName,
            string midiData3HeaderName,
            TranslateMidiMessageType type )
        {
            var column = StartColumn;

            foreach( var message in midiMessages )
            {
                var cellValues = new[] { message.Channel, message.DataByte1, message.DataByte2 };

                for( int i = 0; i < 3; i++ )
                {
                    var flag = (TranslateMidiMessageType)( 1 << i );

                    if( !type.HasFlag( flag ) )
                    {
                        continue;
                    }

                    if( flag.HasFlag( TranslateMidiMessageType.Data2 ) )
                    {
                        var noteName = MidiNoteNameHelper.GetNoteNameList()[ cellValues[ i ].Value ];
                        sheet.Cell( row, column ).Value = noteName;
                    }
                    else
                    {
                        sheet.Cell( row, column ).Value = cellValues[ i ].Value;
                    }
                    column++;
                }
            }

            return column;
        }
    }
}