using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class ProductNameTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<InvalidNameException>( () => IProductNameFactory.Default.Create( "" ) );
            Assert.Throws<InvalidNameException>( () => IProductNameFactory.Default.Create( "  " ) );
            _ = IProductNameFactory.Default.Create( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = IProductNameFactory.Default.Create( "Hoge" );
            var huga = IProductNameFactory.Default.Create( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = IProductNameFactory.Default.Create( "Hoge" );
            var hoge2 = IProductNameFactory.Default.Create( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IProductNameFactory.Default.Create( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( IProductNameFactory.Default.Create( "Hoge" ).ToString() == "Hoge" );
        }

    }
}