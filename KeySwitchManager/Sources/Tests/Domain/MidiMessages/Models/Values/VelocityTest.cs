using System;

using KeySwitchManager.Domain.MidiMessages.Models.Values;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Domain.MidiMessages.Models.Values
{
    [TestFixture]
    public class VelocityTest
    {
        [TestCase( -1 )]
        [TestCase( 128 )]
        public void OutOfRangeTest( int velocity )
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => _ = new MidiVelocity( velocity ) );
        }

        [Test]
        public void EqualityTest()
        {
            var vel1 = new MidiVelocity( 10 );
            var vel2 = new MidiVelocity( 20 );
            Assert.IsTrue( vel1.Equals( new MidiVelocity( 10 ) ) );
            Assert.IsFalse( vel1.Equals( vel2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new MidiVelocity( 1 ).ToString(), "1" );
            Assert.IsTrue( new MidiVelocity( 1 ).ToString() == "1" );
        }
    }
}