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
            var var1 =  new Author( "" );
            var var2 =  new Author( default! );

            Assert.AreEqual( string.Empty, var1.Value );
            Assert.AreEqual( string.Empty, var2.Value );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = new Author( "Hoge" );
            var huga = new Author( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = new Author( "Hoge" );
            var hoge2 = new Author( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new Author( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( new Author( "Hoge" ).ToString() == "Hoge" );
        }

    }
}