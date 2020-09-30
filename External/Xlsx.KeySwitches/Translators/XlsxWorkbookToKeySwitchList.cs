using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;
using KeySwitchManager.UseCases.KeySwitches.Translations;
using KeySwitchManager.Xlsx.KeySwitches.Models;
using KeySwitchManager.Xlsx.KeySwitches.Services;

namespace KeySwitchManager.Xlsx.KeySwitches.Translators
{
    public class XlsxWorkbookToKeySwitchList : IXlsxWorkbookToKeySwitchList
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string Author { get; }
        public string Description { get; }

        public XlsxWorkbookToKeySwitchList(
            string developerName,
            string productName,
            string author = "",
            string description = "" )
        {
            DeveloperName = developerName;
            ProductName   = productName;
            Author        = author;
            Description   = description;
        }

        public IReadOnlyCollection<KeySwitch> Translate( FilePath source )
        {
            var result = new List<KeySwitch>();
            var workbook = XlsxWorkBookParsingService.Parse( source );

            foreach( var sheet in workbook.Worksheets )
            {
                result.Add( TranslateWorkSheet( sheet ) );
            }

            return result;
        }

        private KeySwitch TranslateWorkSheet( Worksheet sheet )
        {
            var now = DateTimeHelper.NowUtc();
            var articulations = new List<Articulation>();

            foreach( var row in sheet.Rows )
            {
                articulations.Add( TranslateArticulation( row ) );
            }

            return IKeySwitchFactory.Default.Create(
                Guid.TryParse( sheet.GuidCell.Value, out var guid ) ? guid : Guid.NewGuid(),
                Author,
                Description,
                now,
                now,
                DeveloperName,
                ProductName,
                sheet.OutputNameCell.Value,
                articulations,
                new Dictionary<string, string>() //TODO
            );
        }

        private Articulation TranslateArticulation( Row row )
        {
            return IArticulationFactory.Default.Create(
                row.ArticulationName.Value,
                TranslateMidiNoteMapping( row ),
                TranslateMidiControlChangeMapping( row ),
                TranslateMidiProgramChangeMapping( row ),
                new Dictionary<string, string>() //TOOD
            );
        }

        private IReadOnlyCollection<MidiNoteOn> TranslateMidiNoteMapping( Row row )
        {
            var result = new List<MidiNoteOn>();
            var factory = IMidiNoteOnFactory.Default;

            foreach( var midiNote in row.MidiNoteList )
            {
                var noteNumber = new MidiNoteName( midiNote.Note.Value ).ToMidiNoteNumber().Value;
                var noteOn = factory.Create( noteNumber, midiNote.Velocity.Value );

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