using System;

using KeySwitchManager.Domain.KeySwitches.Models.Values;

using NUnit.Framework;

namespace KeySwitchManager.Testing.Domain.KeySwitches.Models.Values
{
    [TestFixture]
    public class InstrumentNameTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<ArgumentException>( () => _ = new InstrumentName( "" ) );
            Assert.Throws<ArgumentException>( () => _ = new InstrumentName( "  " ) );
            _ = new InstrumentName( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = new InstrumentName( "Hoge" );
            var huga = new InstrumentName( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = new InstrumentName( "Hoge" );
            var hoge2 = new InstrumentName( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new InstrumentName( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( new InstrumentName( "Hoge" ).ToString() == "Hoge" );
        }

    }
}