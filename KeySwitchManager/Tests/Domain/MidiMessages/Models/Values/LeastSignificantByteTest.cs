using System;

using KeySwitchManager.Domain.MidiMessages.Models.Values;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Domain.MidiMessages.Models.Values
{
    [TestFixture]
    public class LeastSignificantByteTest
    {
        [TestCase( -1 )]
        [TestCase( 256 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => _ = new MidiLeastSignificantByte( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var byte1 = new MidiLeastSignificantByte( 1 );
            var byte2 = new MidiLeastSignificantByte( 2 );
            Assert.IsTrue( byte1.Equals( new MidiLeastSignificantByte( 1 ) ) );
            Assert.IsFalse( byte1.Equals( byte2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new MidiLeastSignificantByte( 1 ).ToString(), "1" );
            Assert.IsTrue( new MidiLeastSignificantByte( 1 ).ToString() == "1" );
        }
    }
}