using System;
using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Aggregations;
using KeySwitchManager.Domain.KeySwitches.Models.Factory;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Factory;
using KeySwitchManager.Domain.MidiMessages.Models.Values;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Translators
{
    public class SpreadsheetImportTranslator : IDataTranslator<Workbook, IReadOnlyCollection<KeySwitch>>
    {
        public IReadOnlyCollection<KeySwitch> Translate( Workbook source )
        {
            var result = new List<KeySwitch>();
            var workbook = source;
            var parsedGuidList = new List<Guid>();

            foreach( var sheet in workbook.Worksheets )
            {
                result.Add( TranslateWorkSheet( sheet, parsedGuidList ) );
            }

            return result;
        }

        private KeySwitch TranslateWorkSheet( Worksheet sheet, ICollection<Guid> parsedGuidList )
        {
            var now = UtcDateTime.NowAsDateTime;
            var articulations = new List<Articulation>();
            var extraData = new Dictionary<string, string>();

            foreach( var row in sheet.Rows )
            {
                articulations.Add( TranslateArticulation( row ) );
            }

            var guid = sheet.GuidCell.Value;

            if( parsedGuidList.Contains( guid ) )
            {
                throw new InvalidOperationException( $"GUID is duplicated in this workbook : {guid}");
            }

            parsedGuidList.Add( guid );

            foreach( var (key, value) in sheet.Extra )
            {
                extraData.Add( key, value.Value );
            }

            return IKeySwitchFactory.Default.Create(
                guid,
                sheet.AuthorCell.Value,
                sheet.DescriptionCell.Value,
                now,
                now,
                sheet.DeveloperNameCell.Value,
                sheet.ProductNameCell.Value,
                sheet.InstrumentNameCell.Value,
                articulations,
                extraData
            );
        }

        private Articulation TranslateArticulation( Row row )
        {
            var extra = new Dictionary<string, string>();

            foreach( var key in row.Extra.Keys )
            {
                extra.Add( key, row.Extra[ key ].Value );
            }

            return IArticulationFactory.Default.Create(
                row.ArticulationName.Value,
                TranslateMidiNoteMapping( row ),
                TranslateMidiControlChangeMapping( row ),
                TranslateMidiProgramChangeMapping( row ),
                extra
            );
        }

        private IReadOnlyCollection<MidiNoteOn> TranslateMidiNoteMapping( Row row )
        {
            var result = new List<MidiNoteOn>();
            var factory = IMidiNoteOnFactory.Default;

            foreach( var midiNote in row.MidiNoteList )
            {
                var channel = midiNote.Channel.Value;
                var noteNumber = IMidiNoteNameFactory.Default.Create( midiNote.Note.Value ).ToMidiNoteNumber().Value;
                var noteOn = factory.Create( channel, noteNumber, midiNote.Velocity.Value );

                result.Add( noteOn );
            }

            return result;
        }

        private IReadOnlyCollection<MidiControlChange> TranslateMidiControlChangeMapping( Row row )
        {
            var result = new List<MidiControlChange>();
            var factory = IMidiControlChangeFactory.Default;

            foreach( var cc in row.MidiControlChangeList )
            {
                var controlChange = factory.Create(
                    cc.Channel.Value,
                    cc.CcNumber.Value,
                    cc.CcValue.Value
                );

                result.Add( controlChange );
            }

            return result;
        }

        private IReadOnlyCollection<MidiProgramChange> TranslateMidiProgramChangeMapping( Row row )
        {
            var result = new List<MidiProgramChange>();
            var factory = IMidiProgramChangeFactory.Default;

            foreach( var pc in row.MidiProgramChangeList )
            {
                var programChange = factory.Create(
                    pc.Channel.Value,
                    pc.Data.Value
                );

                result.Add( programChange );
            }

            return result;
        }

    }
}
