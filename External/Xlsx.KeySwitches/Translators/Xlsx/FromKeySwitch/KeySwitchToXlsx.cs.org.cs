// using System.Collections.Generic;
//
// using KeySwitchManager.Domain.Commons;
// using KeySwitchManager.Domain.KeySwitches.Aggregate;
// using KeySwitchManager.Domain.MidiMessages.Aggregate;
// using KeySwitchManager.Domain.Translations;
// using KeySwitchManager.Xlsx.KeySwitches.Models;
//
// namespace KeySwitchManager.Xlsx.KeySwitches.Translators
// {
//     public class KeySwitchToXlsx : IDataTranslator<IReadOnlyCollection<KeySwitch>, Workbook>
//     {
//         public Workbook Translate( IReadOnlyCollection<KeySwitch> keySwitches )
//         {
//             var result = new Workbook();
//
//             foreach( var k in keySwitches )
//             {
//                 result.Worksheets.Add( TranslateWorkSheet( k ) );
//             }
//
//             return result;
//         }
//
//         private static Worksheet TranslateWorkSheet( KeySwitch keySwitch )
//         {
//             var result = new Worksheet( keySwitch.InstrumentName.Value )
//             {
//                 GuidCell = new GuidCell( keySwitch.Id.ToString() )
//             };
//
//             foreach( var articulation in keySwitch.Articulations )
//             {
//                 result.Rows.Add( TranslateArticulation( articulation ) );
//             }
//
//             TranslateExtra( keySwitch, result.Extra );
//
//             return result;
//         }
//
//         private static Row TranslateArticulation( Articulation articulation )
//         {
//             var result = new Row( new ArticulationNameCell( articulation.ArticulationName.Value ) );
//             result.MidiNoteList.AddRange( TranslateMidiNoteMapping( articulation.MidiNoteOns ) );
//             result.MidiControlChangeList.AddRange( TranslateMidiControlChangeMapping( articulation.MidiControlChanges ) );
//             result.MidiProgramChangeList.AddRange( TranslateMidiProgramChangeMapping( articulation.MidiProgramChanges ) );
//
//             return result;
//         }
//
//         private static IReadOnlyCollection<Row.MidiNote> TranslateMidiNoteMapping( IDataList<IMidiMessage> midiMessages )
//         {
//             var result = new List<Row.MidiNote>();
//             var noteNameList = MidiNoteNumberCell.GetNoteNameList();
//
//             foreach( var message in midiMessages )
//             {
//                 var note = new Row.MidiNote
//                 {
//                     Note     = new MidiNoteNumberCell( noteNameList[ message.DataByte1.Value ] ),
//                     Velocity = new MidiNoteVelocityCell( message.DataByte2.Value )
//                 };
//
//                 result.Add( note );
//             }
//
//             return result;
//         }
//
//         private static IReadOnlyCollection<Row.MidiControlChange> TranslateMidiControlChangeMapping( IDataList<IMidiMessage> midiMessages )
//         {
//             var result = new List<Row.MidiControlChange>();
//
//             foreach( var message in midiMessages )
//             {
//                 var note = new Row.MidiControlChange
//                 {
//                     CcNumber = new MidiControlChangeNumberCell( message.DataByte1.Value ),
//                     CcValue  = new MidiControlChangeValueCell( message.DataByte2.Value )
//                 };
//
//                 result.Add( note );
//             }
//
//             return result;
//         }
//
//         private static IReadOnlyCollection<Row.MidiProgramChange> TranslateMidiProgramChangeMapping( IDataList<IMidiMessage> midiMessages )
//         {
//             var result = new List<Row.MidiProgramChange>();
//
//             foreach( var message in midiMessages )
//             {
//                 var note = new Row.MidiProgramChange
//                 {
//                     Channel = new MidiProgramChangeCell( message.DataByte1.Value ),
//                     Data    = new MidiProgramChangeCell( message.DataByte2.Value )
//                 };
//
//                 result.Add( note );
//             }
//
//             return result;
//         }
//
//         private static void TranslateExtra( KeySwitch keySwitch, Dictionary<string, ExtraDataCell> resultExtra )
//         {
//             // TODO Extra data in worksheet is reserved future
//         }
//
//     }
// }