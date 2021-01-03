using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class ExtraDataValueTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<NullOrEmptyException>( () => IExtraDataValueFactory.Default.Create( "" ) );
            Assert.Throws<NullOrEmptyException>( () => IExtraDataValueFactory.Default.Create( "  " ) );
            _ = IExtraDataValueFactory.Default.Create( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = IExtraDataValueFactory.Default.Create( "Hoge" );
            var huga = IExtraDataValueFactory.Default.Create( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = IExtraDataValueFactory.Default.Create( "Hoge" );
            var hoge2 = IExtraDataValueFactory.Default.Create( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IExtraDataValueFactory.Default.Create( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( IExtraDataValueFactory.Default.Create( "Hoge" ).ToString() == "Hoge" );
        }

    }
}