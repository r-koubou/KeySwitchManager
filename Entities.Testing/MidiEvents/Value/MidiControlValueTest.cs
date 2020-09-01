using ArticulationManager.Entities.MidiEventData.Value;
using ArticulationManager.Utilities;

using NUnit.Framework;

namespace Entities.Testing.MidiEvents.Value
{
    [TestFixture]
    public class MidiControlValueTest
    {
        [Test]
        [TestCase( MidiControlChangeValue.MinValue - 1 )]
        [TestCase( MidiControlChangeValue.MaxValue + 1 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new MidiControlChangeValue( ccNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var cc1 = new MidiControlChangeValue( 1 );
            var cc2 = new MidiControlChangeValue( 2 );
            Assert.IsTrue( cc1.Equals( new MidiControlChangeValue( 1 ) ) );
            Assert.IsFalse( cc1.Equals( cc2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new MidiControlChangeValue( 1 ).ToString(), "1" );
            Assert.IsTrue( new MidiControlChangeValue( 1 ).ToString() == "1" );

        }

    }
}