using ArticulationManager.Common.Utilities;
using ArticulationManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class ChannelTest
    {
        [TestCase( Channel.MinValue - 1 )]
        [TestCase( Channel.MaxValue + 1 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new Channel( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var vel1 = new Channel( 10 );
            var vel2 = new Channel( 12 );
            Assert.IsTrue( vel1.Equals( new Channel( 10 ) ) );
            Assert.IsFalse( vel1.Equals( vel2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new Channel( 1 ).ToString(), "1" );
            Assert.IsTrue( new Channel( 1 ).ToString() == "1" );
        }
    }
}