using ArticulationManager.Entities.MidiEventData.Value;
using ArticulationManager.Utilities;

using NUnit.Framework;

namespace Entities.Testing.MidiEvents.Value
{
    [TestFixture]
    public class MidiMostSignificantByteTest
    {
        [TestCase( MidiMostSignificantByte.MinValue - 1 )]
        [TestCase( MidiMostSignificantByte.MaxValue + 1 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new MidiMostSignificantByte( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var byte1 = new MidiMostSignificantByte( 1 );
            var byte2 = new MidiMostSignificantByte( 2 );
            Assert.IsTrue( byte1.Equals( new MidiMostSignificantByte( 1 ) ) );
            Assert.IsFalse( byte1.Equals( byte2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new MidiMostSignificantByte( 1 ).ToString(), "1" );
            Assert.IsTrue( new MidiMostSignificantByte( 1 ).ToString() == "1" );
        }
    }
}