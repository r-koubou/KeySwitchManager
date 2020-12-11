using System.Collections.Generic;

using ClosedXML.Excel;

using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Xlsx.KeySwitches.Models;

namespace KeySwitchManager.Xlsx.KeySwitches.Translators.FromKeySwitch
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
                var headerNames = new[] { midiData1HeaderName, midiData2HeaderName, midiData3HeaderName };

                #warning TODO Domain Object midi data name update to data1, data2, data3 style
                // NOW: chanel, data1, data2
                var cellValues  = new[] { message.Channel, message.DataByte1, message.DataByte2 };

                for( int i = 0; i < 3; i++ )
                {
                    var flag = (TranslateMidiMessageType)( 1 << i );

                    if( !type.HasFlag( flag ) )
                    {
                        continue;
                    }

                    if( flag.HasFlag( TranslateMidiMessageType.Data2 ) )
                    {
                        var noteName = MidiNoteNumberCell.GetNoteNameList()[ cellValues[ i ].Value ];
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