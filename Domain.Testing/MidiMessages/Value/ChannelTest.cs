using System;

using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class ChannelTest
    {
        [TestCase( -1 )]
        [TestCase( 16 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => IMidiChannelFactory.Default.Create( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var vel1 = IMidiChannelFactory.Default.Create( 10 );
            var vel2 = IMidiChannelFactory.Default.Create( 12 );
            Assert.IsTrue( vel1.Equals( IMidiChannelFactory.Default.Create( 10 ) ) );
            Assert.IsFalse( vel1.Equals( vel2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IMidiChannelFactory.Default.Create( 1 ).ToString(), "1" );
            Assert.IsTrue( IMidiChannelFactory.Default.Create( 1 ).ToString() == "1" );
        }
    }
}