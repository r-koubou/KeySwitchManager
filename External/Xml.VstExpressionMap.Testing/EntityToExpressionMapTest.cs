using System;
using System.Collections.Generic;

using KeySwitchManager.Common.Testing;
using KeySwitchManager.Domain.MidiMessages;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Xml.VstExpressionMap.Translations;

using NUnit.Framework;

namespace KeySwitchManager.Xml.VstExpressionMap.Testing
{
    [TestFixture]
    public class EntityToExpressionMapTest
    {
        [Test]
        public void ConvertTest()
        {
            var articulation = TestDataGenerator.CreateArticulation(
                new List<MidiNoteOn>
                {
                    IMidiNoteOnFactory.Default.Create( 0, 10, 20 )
                },
                new List<MidiControlChange>{},
                new List<MidiProgramChange>{}
            );

            var entity = TestDataGenerator.CreateKeySwitch( articulation );
            var translator = new KeySwitchToVstExpressionMapModel();
            var text = translator.Translate( entity );

            Console.WriteLine( text );
        }
    }
}