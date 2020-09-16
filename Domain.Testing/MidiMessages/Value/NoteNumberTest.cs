using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.MidiMessages.Value;

using NUnit.Framework;

namespace Domain.Testing.MidiMessages.Value
{
    [TestFixture]
    public class NoteNumberTest
    {
        [TestCase( NoteNumber.MinValue - 1 )]
        [TestCase( NoteNumber.MaxValue + 1 )]
        public void OutOfRangeTest( int noteNumber )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new NoteNumber( noteNumber ) );
        }

        [Test]
        public void EqualityTest()
        {
            var note1 = new NoteNumber( 1 );
            var note2 = new NoteNumber( 2 );
            Assert.IsTrue( note1.Equals( new NoteNumber( 1 ) ) );
            Assert.IsFalse( note1.Equals( note2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new NoteNumber( 1 ).ToString(), "1" );
            Assert.IsTrue( new NoteNumber( 1 ).ToString() == "1" );
        }
    }
}