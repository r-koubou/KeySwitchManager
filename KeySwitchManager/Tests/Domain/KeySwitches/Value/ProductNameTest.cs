using System;

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
            Assert.Throws<ArgumentException>( () => _ = new ProductName( "" ) );
            Assert.Throws<ArgumentException>( () => _ = new ProductName( "  " ) );
            _ = new ProductName( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = new ProductName( "Hoge" );
            var huga = new ProductName( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = new ProductName( "Hoge" );
            var hoge2 = new ProductName( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new ProductName( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( new ProductName( "Hoge" ).ToString() == "Hoge" );
        }

    }
}