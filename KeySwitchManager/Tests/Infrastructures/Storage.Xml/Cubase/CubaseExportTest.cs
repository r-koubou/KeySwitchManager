using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Midi.Models;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Infrastructures.Storage.Xml.Cubase.Translators;
using KeySwitchManager.Testing.Commons.KeySwitches;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Xml.Cubase
{
    [TestFixture]
    public class CubaseExportTest
    {
        [Test]
        public void ConvertTest()
        {
            var articulation = TestDataGenerator.CreateArticulation(
                new List<MidiNoteOn>
                {
                    IMidiNoteOnFactory.Default.Create( 10, 20 )
                },
                new List<MidiControlChange>{},
                new List<MidiProgramChange>{}
            );

            var entity = TestDataGenerator.CreateKeySwitch( articulation );
            var translator = new CubaseExportTranslator();
            var text = translator.Translate( entity );

            Console.WriteLine( text );
        }
    }
}