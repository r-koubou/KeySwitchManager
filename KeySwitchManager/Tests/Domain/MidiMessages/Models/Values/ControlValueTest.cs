using System;

using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Domain.MidiMessages.Models.Values
{
    [TestFixture]
    public class ControlValueTest
    {
        [Test]
        [TestCase( -1 )]
        [TestCase( 128 )]
        public void OutOfRangeTest( int ccNumber )
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => _ = new MidiControlChangeValue( ccNumber ) );
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