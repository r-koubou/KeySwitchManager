using System;

using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class ControlValueTest
    {
        [Test]
        [TestCase( -1 )]
        [TestCase( 128 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => IMidiControlChangeValueFactory.Default.Create( ccNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var cc1 = IMidiControlChangeNumberFactory.Default.Create( 1 );
            var cc2 = IMidiControlChangeNumberFactory.Default.Create( 2 );
            Assert.IsTrue( cc1.Equals( IMidiControlChangeNumberFactory.Default.Create( 1 ) ) );
            Assert.IsFalse( cc1.Equals( cc2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IMidiControlChangeNumberFactory.Default.Create( 1 ).ToString(), "1" );
            Assert.IsTrue( IMidiControlChangeNumberFactory.Default.Create( 1 ).ToString() == "1" );

        }

    }
}