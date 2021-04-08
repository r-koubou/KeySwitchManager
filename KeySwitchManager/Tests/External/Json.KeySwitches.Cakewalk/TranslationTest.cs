using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Json.KeySwitches.Cakewalk.Translators;

using NUnit.Framework;

namespace KeySwitchManager.Json.KeySwitches.Cakewalk.Testing
{
    [TestFixture]
    public class TranslationTest
    {
        [Test]
        public void TranslateTest()
        {
            var artichlation = TestDataGenerator.CreateArticulation(
                new List<MidiNoteOn>()
                {
                    IMidiNoteOnFactory.Default.Create( 0, 123 )
                },
                new List<MidiControlChange>(),
                new List<MidiProgramChange>()
            );
            var keySwitch = TestDataGenerator.CreateKeySwitch( artichlation );
            var translator = new KeySwitchToCakewalkArticulationModel();
            var jsonText = translator.Translate( keySwitch );

            Console.WriteLine( jsonText );
        }
    }
}
