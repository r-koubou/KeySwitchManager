using System;

using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class MostSignificantByteTest
    {
        [TestCase( -1 )]
        [TestCase( 256 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => IMidiMostSignificantByteFactory.Default.Create( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var byte1 = IMidiMostSignificantByteFactory.Default.Create( 1 );
            var byte2 = IMidiMostSignificantByteFactory.Default.Create( 2 );
            Assert.IsTrue( byte1.Equals( IMidiMostSignificantByteFactory.Default.Create( 1 ) ) );
            Assert.IsFalse( byte1.Equals( byte2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IMidiMostSignificantByteFactory.Default.Create( 1 ).ToString(), "1" );
            Assert.IsTrue( IMidiMostSignificantByteFactory.Default.Create( 1 ).ToString() == "1" );
        }
    }
}