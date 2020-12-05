using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Xlsx.KeySwitches;

using NUnit.Framework;

namespace KeySwitchManager.Interactors.Testing.SpreadSheet
{
    [TestFixture]
    public class SpreadSheetTemplateExportingInteractorTest
    {
        [Test]
        public void ExportTest()
        {
            var articulations = new List<Articulation>();
            var midiNotes = new List<IMidiMessage>();
            var midiCcs = new List<IMidiMessage>();
            var midiPcs = new List<IMidiMessage>();

            midiNotes.Add( IMidiNoteOnFactory.Default.Create( 1, 100 ) );
            midiNotes.Add( IMidiNoteOnFactory.Default.Create( 2, 100 ) );

            midiCcs.Add( IMidiControlChangeFactory.Default.Create( 1, 100 ) );
            midiCcs.Add( IMidiControlChangeFactory.Default.Create( 2, 100 ) );

            midiPcs.Add( IMidiProgramChangeFactory.Default.Create( 1, 100 ) );
            midiPcs.Add( IMidiProgramChangeFactory.Default.Create( 2, 100 ) );

            articulations.Add(
                IArticulationFactory.Default.Create(
                    "Power Chord",
                    midiNotes,
                    midiCcs,
                    midiPcs,
                    new Dictionary<string, string>
                    {
                        { "Ext.Key1", "value1" },
                        { "Ext.Key2", "value2" },
                    }
                )
            );

            articulations.Add(
                IArticulationFactory.Default.Create(
                    "4th Chord",
                    midiNotes,
                    midiCcs,
                    midiPcs,
                    new Dictionary<string, string>
                    {
                        { "Ext.Key1", "value12" },
                        { "Ext.Key3", "value3" },
                    }
                )
            );

            var keySwitch = IKeySwitchFactory.Default.Create(
                Guid.NewGuid(),
                "Author",
                "Test",
                DateTime.Now,
                DateTime.Now,
                "Developer",
                "Product",
                "Guitar",
                articulations,
                new Dictionary<string, string>()
            );

            var repository = new XlsxExportingRepository( new FilePath( "out.xlsx" ) );
            var result = repository.Save( new []{ keySwitch } );

            Assert.IsTrue( result );
        }
    }
}