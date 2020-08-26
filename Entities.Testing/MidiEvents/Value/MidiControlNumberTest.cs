using ArticulationManager.Entities.MidiEvent.Value;
using ArticulationManager.Utilities;

using NUnit.Framework;

namespace Entities.Testing.MidiEvents.Value
{
    [TestFixture]
    public class MidiControlNumberTest
    {
        [Test]
        [TestCase( MidiControlChangeNumber.MinValue - 1 )]
        [TestCase( MidiControlChangeNumber.MaxValue + 1 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new MidiControlChangeNumber( ccNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var cc1 = new MidiControlChangeNumber( 1 );
            var cc2 = new MidiControlChangeNumber( 2 );
            Assert.IsTrue( cc1.Equals( new MidiControlChangeNumber( 1 ) ) );
            Assert.IsFalse( cc1.Equals( cc2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new MidiControlChangeNumber( 1 ).ToString(), "1" );
            Assert.IsTrue( new MidiControlChangeNumber( 1 ).ToString() == "1" );
        }
    }
}