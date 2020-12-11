using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class StatusCodeTest
    {
        [Test]
        [TestCase( MidiStatus.MinValue - 1 )]
        [TestCase( MidiStatus.MaxValue + 1 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new MidiStatus( ccNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var code1 = new MidiStatus( 1 );
            var code2 = new MidiStatus( 2 );
            Assert.IsTrue( code1.Equals( new MidiStatus( 1 ) ) );
            Assert.IsFalse( code1.Equals( code2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new MidiStatus( 1 ).ToString(), "1" );
            Assert.IsTrue( new MidiStatus( 1 ).ToString() == "1" );

        }

    }
}