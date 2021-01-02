using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class StatusCodeTest
    {
        [Test]
        [TestCase( -1 )]
        [TestCase( 256 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => IMidiStatusFactory.Default.Create( ccNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var code1 = IMidiStatusFactory.Default.Create( 1 );
            var code2 = IMidiStatusFactory.Default.Create( 2 );
            Assert.IsTrue( code1.Equals( IMidiStatusFactory.Default.Create( 1 ) ) );
            Assert.IsFalse( code1.Equals( code2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IMidiStatusFactory.Default.Create( 1 ).ToString(), "1" );
            Assert.IsTrue( IMidiStatusFactory.Default.Create( 1 ).ToString() == "1" );

        }

    }
}