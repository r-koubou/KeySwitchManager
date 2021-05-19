using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Midi.Models;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Storage.Yaml.KeySwitches.Translators;
using KeySwitchManager.Testing.Commons.KeySwitches;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Yaml.KeySwitches
{
    [TestFixture]
    public class KeySwitchExportTest
    {
        [Test]
        public void ConvertToYamlTest()
        {
            var midiNoteFactory = IMidiNoteOnFactory.Default;
            var midiCcFactory = IMidiControlChangeFactory.Default;
            var midiPcFactory = IMidiProgramChangeFactory.Default;

            var articulation = TestDataGenerator.CreateArticulation(
                new List<MidiNoteOn> { midiNoteFactory.Create( 1, 23 ) },
                new List<MidiControlChange> { midiCcFactory.Create( 2, 34 ) },
                new List<MidiProgramChange> { midiPcFactory.Create( 3, 45 ) }
            );

            var entity = TestDataGenerator.CreateKeySwitch( articulation );

            var translator = new YamlKeySwitchExportTranslator();
            var yaml = translator.Translate( new[] { entity } );

            var translator2 = new YamlKeySwitchImportTranslator();
            var cmp = translator2.Translate( yaml );

            Assert.AreEqual( new[] { entity }, cmp );

            Console.WriteLine( yaml );
        }
    }
}