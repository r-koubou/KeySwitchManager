using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class ExtraDataKeyTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<NullOrEmptyException>( () =>  IExtraDataKeyFactory.Default.Create( "" ) );
            Assert.Throws<NullOrEmptyException>( () =>  IExtraDataKeyFactory.Default.Create( "  " ) );
            _ = IExtraDataKeyFactory.Default.Create( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = new ExtraDataKey( "Hoge" );
            var huga = new ExtraDataKey( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = new ExtraDataKey( "Hoge" );
            var hoge2 = new ExtraDataKey( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new ExtraDataKey( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( new ExtraDataKey( "Hoge" ).ToString() == "Hoge" );
        }

    }
}