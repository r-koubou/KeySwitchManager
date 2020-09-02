using ArticulationManager.Common.Utilities;
using ArticulationManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class StatusCodeTest
    {
        [Test]
        [TestCase( StatusCode.MinValue - 1 )]
        [TestCase( StatusCode.MaxValue + 1 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new StatusCode( ccNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var code1 = new StatusCode( 1 );
            var code2 = new StatusCode( 2 );
            Assert.IsTrue( code1.Equals( new StatusCode( 1 ) ) );
            Assert.IsFalse( code1.Equals( code2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new StatusCode( 1 ).ToString(), "1" );
            Assert.IsTrue( new StatusCode( 1 ).ToString() == "1" );

        }

    }
}