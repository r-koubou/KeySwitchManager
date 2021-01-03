using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class LeastSignificantByteTest
    {
        [TestCase( -1 )]
        [TestCase( 256 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => IMidiLeastSignificantByteFactory.Default.Create( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var byte1 = IMidiLeastSignificantByteFactory.Default.Create( 1 );
            var byte2 = IMidiLeastSignificantByteFactory.Default.Create( 2 );
            Assert.IsTrue( byte1.Equals( IMidiLeastSignificantByteFactory.Default.Create( 1 ) ) );
            Assert.IsFalse( byte1.Equals( byte2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IMidiLeastSignificantByteFactory.Default.Create( 1 ).ToString(), "1" );
            Assert.IsTrue( IMidiLeastSignificantByteFactory.Default.Create( 1 ).ToString() == "1" );
        }
    }
}