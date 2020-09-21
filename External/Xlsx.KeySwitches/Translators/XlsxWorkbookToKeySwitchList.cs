using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
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

        public IEnumerable<KeySwitch> Translate( FilePath source )
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

            return new IKeySwitchFactory.Default().Create(
                Guid.NewGuid(),
                Author,
                Description,
                now,
                now,
                DeveloperName,
                ProductName,
                sheet.Name,
                articulations
            );
        }

        private Articulation TranslateArticulation( Row row )
        {
            return new IArticulationFactory.Default().Create(
                row.ArticulationName.Value,
                EnumHelper.Parse<ArticulationType>( row.ArticulationType.Value ),
                row.GroupIndex.Value,
                row.ColorIndex.Value,
                TranslateMidiNoteMapping( row ),
                TranslateMidiControlChangeMapping( row ),
                TranslateMidiProgramChangeMapping( row )
            );
        }

        private IEnumerable<NoteOn> TranslateMidiNoteMapping( Row row )
        {
            var result = new List<NoteOn>();
            var factory = new INoteOnFactory.Default();

            foreach( var midiNote in row.MidiNoteList )
            {
                var noteNumber = new MidiNoteName( midiNote.Note.Value ).ToMidiNoteNumber().Value;
                var noteOn = factory.Create( noteNumber, midiNote.Velocity.Value );

                result.Add( noteOn );
            }

            return result;
        }

        private IEnumerable<ControlChange> TranslateMidiControlChangeMapping( Row row )
        {
            var result = new List<ControlChange>();
            var factory = IControlChangeFactory.DefaultFactory;

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

        private IEnumerable<ProgramChange> TranslateMidiProgramChangeMapping( Row row )
        {
            var result = new List<ProgramChange>();
            var factory = IProgramChangeFactory.DefaultFactory;

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