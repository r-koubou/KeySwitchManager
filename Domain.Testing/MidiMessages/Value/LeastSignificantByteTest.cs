using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class LeastSignificantByteTest
    {
        [TestCase( LeastSignificantByte.MinValue - 1 )]
        [TestCase( LeastSignificantByte.MaxValue + 1 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new LeastSignificantByte( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var byte1 = new LeastSignificantByte( 1 );
            var byte2 = new LeastSignificantByte( 2 );
            Assert.IsTrue( byte1.Equals( new LeastSignificantByte( 1 ) ) );
            Assert.IsFalse( byte1.Equals( byte2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new LeastSignificantByte( 1 ).ToString(), "1" );
            Assert.IsTrue( new LeastSignificantByte( 1 ).ToString() == "1" );
        }
    }
}