using System;

using KeySwitchManager.Domain.MidiMessages.Models;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Factory;
using KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Translators;
using KeySwitchManager.Testing.Commons.KeySwitches;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Storage.Xml.StudioOne
{
    [TestFixture]
    public class StudioOneExportTest
    {
        [Test]
        public void TranslateTest()
        {
            var translator = new StudioOneExportTranslator();
            var articulation = TestDataGenerator.CreateArticulation(
                new MidiNoteOn[]{ IMidiNoteOnFactory.Default.Create( 1, 23 ) },
                new MidiControlChange[]{},
                new MidiProgramChange[]{}
            );
            var entity = TestDataGenerator.CreateKeySwitch( articulation );
            var xml = translator.Translate( entity );

            Console.Out.WriteLine( xml );
        }
    }
}