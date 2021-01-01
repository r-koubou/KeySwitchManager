using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class InstrumentNameTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<InvalidNameException>( () => IInstrumentNameFactory.Default.Create( "" ) );
            Assert.Throws<InvalidNameException>( () => IInstrumentNameFactory.Default.Create( "  " ) );
            _ = IInstrumentNameFactory.Default.Create( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = IInstrumentNameFactory.Default.Create( "Hoge" );
            var huga = IInstrumentNameFactory.Default.Create( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = IInstrumentNameFactory.Default.Create( "Hoge" );
            var hoge2 = IInstrumentNameFactory.Default.Create( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IInstrumentNameFactory.Default.Create( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( IInstrumentNameFactory.Default.Create( "Hoge" ).ToString() == "Hoge" );
        }

    }
}