using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

using RkHelper.Text;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class InstrumentNameTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<EmptyTextException>( () => _ = new InstrumentName( "" ) );
            Assert.Throws<EmptyTextException>( () => _ = new InstrumentName( "  " ) );
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