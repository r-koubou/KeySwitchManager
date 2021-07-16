using System;

using KeySwitchManager.Domain.MidiMessages.Models.Values;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Domain.MidiMessages.Models.Values
{
    [TestFixture]
    public class NoteNameTest
    {
        [TestCase( "C-2" )]
        [TestCase( "C#-2" )]
        [TestCase( "C0" )]
        [TestCase( "G8" )]
        public void ValidByNameTest( string name )
        {
            Assert.DoesNotThrow( () =>
            {
                var _ = new MidiNoteName( name );
            });
        }

        [TestCase( "C-4" )]
        [TestCase( "Z-2" )]
        [TestCase( "G9" )]
        public void InValidByNameTest( string name )
        {
            Assert.Throws<ArgumentException>( () =>
            {
                var _ = new MidiNoteName( name );
            });
        }

        [TestCase( 0 )]
        [TestCase( 127 )]
        public void ValidByNumberTest( int noteNumber )
        {
            Assert.DoesNotThrow( () =>
            {
                var _ = MidiNoteName.FromMidiNoteNumber( new MidiNoteNumber( noteNumber ) );
            });
        }
    }
}
