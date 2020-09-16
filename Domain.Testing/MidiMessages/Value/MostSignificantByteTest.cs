using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class MostSignificantByteTest
    {
        [TestCase( MostSignificantByte.MinValue - 1 )]
        [TestCase( MostSignificantByte.MaxValue + 1 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new MostSignificantByte( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var byte1 = new MostSignificantByte( 1 );
            var byte2 = new MostSignificantByte( 2 );
            Assert.IsTrue( byte1.Equals( new MostSignificantByte( 1 ) ) );
            Assert.IsFalse( byte1.Equals( byte2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new MostSignificantByte( 1 ).ToString(), "1" );
            Assert.IsTrue( new MostSignificantByte( 1 ).ToString() == "1" );
        }
    }
}