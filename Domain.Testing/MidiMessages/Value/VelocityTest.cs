using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class VelocityTest
    {
        [TestCase( Velocity.MinValue - 1 )]
        [TestCase( Velocity.MaxValue + 1 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new Velocity( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var vel1 = new Velocity( 10 );
            var vel2 = new Velocity( 20 );
            Assert.IsTrue( vel1.Equals( new Velocity( 10 ) ) );
            Assert.IsFalse( vel1.Equals( vel2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new Velocity( 1 ).ToString(), "1" );
            Assert.IsTrue( new Velocity( 1 ).ToString() == "1" );
        }
    }
}