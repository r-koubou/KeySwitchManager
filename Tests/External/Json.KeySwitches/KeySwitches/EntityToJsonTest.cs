using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Testing.KeySwitches;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Interactors.Testing.KeySwitches;
using KeySwitchManager.Json.KeySwitches.Translations;

using NUnit.Framework;

namespace Json.KeySwitches.Testing.KeySwitches
{
    [TestFixture]
    public class EntityToJsonTest
    {
        [Test]
        public void ConvertToJsonTest()
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

            var translator = new KeySwitchToJsonModel();
            var json = translator.Translate( entity );

            var translator2 = new JsonModelToKeySwitch();
            var cmp = translator2.Translate( json );

            Assert.AreEqual( entity, cmp );

            Console.WriteLine( json );
        }
    }
}