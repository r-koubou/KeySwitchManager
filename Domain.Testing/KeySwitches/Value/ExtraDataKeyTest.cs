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
            var hoge = IExtraDataKeyFactory.Default.Create( "Hoge" );
            var huga = IExtraDataKeyFactory.Default.Create( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = IExtraDataKeyFactory.Default.Create( "Hoge" );
            var hoge2 = IExtraDataKeyFactory.Default.Create( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IExtraDataKeyFactory.Default.Create( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( IExtraDataKeyFactory.Default.Create( "Hoge" ).ToString() == "Hoge" );
        }

    }
}