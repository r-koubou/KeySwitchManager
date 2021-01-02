using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class VelocityTest
    {
        [TestCase( -1 )]
        [TestCase( 128 )]
        public void OutOfRangeTest( int velocity )
        {
            Assert.Throws<ValueOutOfRangeException>( () => IMidiVelocityFactory.Default.Create( velocity ) );
        }

        [Test]
        public void EqualityTest()
        {
            var vel1 = IMidiVelocityFactory.Default.Create( 10 );
            var vel2 = IMidiVelocityFactory.Default.Create( 20 );
            Assert.IsTrue( vel1.Equals( IMidiVelocityFactory.Default.Create( 10 ) ) );
            Assert.IsFalse( vel1.Equals( vel2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IMidiVelocityFactory.Default.Create( 1 ).ToString(), "1" );
            Assert.IsTrue( IMidiVelocityFactory.Default.Create( 1 ).ToString() == "1" );
        }
    }
}