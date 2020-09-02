using ArticulationManager.Common.Utilities;
using ArticulationManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class ControlNumberTest
    {
        [Test]
        [TestCase( ControlChangeNumber.MinValue - 1 )]
        [TestCase( ControlChangeNumber.MaxValue + 1 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new ControlChangeNumber( ccNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var cc1 = new ControlChangeNumber( 1 );
            var cc2 = new ControlChangeNumber( 2 );
            Assert.IsTrue( cc1.Equals( new ControlChangeNumber( 1 ) ) );
            Assert.IsFalse( cc1.Equals( cc2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new ControlChangeNumber( 1 ).ToString(), "1" );
            Assert.IsTrue( new ControlChangeNumber( 1 ).ToString() == "1" );
        }
    }
}