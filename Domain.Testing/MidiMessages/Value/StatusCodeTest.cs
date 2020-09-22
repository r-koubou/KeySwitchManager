using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class StatusCodeTest
    {
        [Test]
        [TestCase( MidiStatusCode.MinValue - 1 )]
        [TestCase( MidiStatusCode.MaxValue + 1 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new MidiStatusCode( ccNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var code1 = new MidiStatusCode( 1 );
            var code2 = new MidiStatusCode( 2 );
            Assert.IsTrue( code1.Equals( new MidiStatusCode( 1 ) ) );
            Assert.IsFalse( code1.Equals( code2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new MidiStatusCode( 1 ).ToString(), "1" );
            Assert.IsTrue( new MidiStatusCode( 1 ).ToString() == "1" );

        }

    }
}