using System;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Interactors.Testing.KeySwitches;
using KeySwitchManager.Xml.StudioOne.KeySwitch.Translations;

using NUnit.Framework;

namespace KeySwitchManager.Xml.StudioOne.KeySwitch.Testing
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