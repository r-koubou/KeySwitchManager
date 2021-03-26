using System;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Xml.KeySwitches.StudioOne.Translators;

using NUnit.Framework;

namespace KeySwitchManager.Xml.KeySwitch.StudioOne.Testing
{
    [TestFixture]
    public class KeySwitchToStudioOneKeySwitchTest
    {
        [Test]
        public void TranslateTest()
        {
            var translator = new KeySwitchToStudioOneKeySwitchModel();
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