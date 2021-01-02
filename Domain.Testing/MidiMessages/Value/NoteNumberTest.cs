using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class NoteNumberTest
    {
        [TestCase( -1 )]
        [TestCase( 128 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => IMidiNoteNumberFactory.Default.Create( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var note1 = IMidiNoteNumberFactory.Default.Create( 1 );
            var note2 = IMidiNoteNumberFactory.Default.Create( 2 );
            Assert.IsTrue( note1.Equals( IMidiNoteNumberFactory.Default.Create( 1 ) ) );
            Assert.IsFalse( note1.Equals( note2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IMidiNoteNumberFactory.Default.Create( 1 ).ToString(), "1" );
            Assert.IsTrue( IMidiNoteNumberFactory.Default.Create( 1 ).ToString() == "1" );
        }
    }
}