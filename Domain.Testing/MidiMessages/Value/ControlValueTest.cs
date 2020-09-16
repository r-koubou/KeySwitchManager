using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class ControlValueTest
    {
        [Test]
        [TestCase( ControlChangeValue.MinValue - 1 )]
        [TestCase( ControlChangeValue.MaxValue + 1 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new ControlChangeValue( ccNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var cc1 = new ControlChangeValue( 1 );
            var cc2 = new ControlChangeValue( 2 );
            Assert.IsTrue( cc1.Equals( new ControlChangeValue( 1 ) ) );
            Assert.IsFalse( cc1.Equals( cc2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new ControlChangeValue( 1 ).ToString(), "1" );
            Assert.IsTrue( new ControlChangeValue( 1 ).ToString() == "1" );

        }

    }
}