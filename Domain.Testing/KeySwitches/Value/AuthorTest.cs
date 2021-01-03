using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class AuthorTest
    {
        [Test]
        public void EmptyNameTest()
        {
            var var1 =  IAuthorFactory.Default.Create( "" );
            var var2 =  IAuthorFactory.Default.Create( default! );

            Assert.AreEqual( string.Empty, var1.Value );
            Assert.AreEqual( string.Empty, var2.Value );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge =IAuthorFactory.Default.Create( "Hoge" );
            var huga =IAuthorFactory.Default.Create( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 =IAuthorFactory.Default.Create( "Hoge" );
            var hoge2 =IAuthorFactory.Default.Create( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual(IAuthorFactory.Default.Create( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue(IAuthorFactory.Default.Create( "Hoge" ).ToString() == "Hoge" );
        }

    }
}