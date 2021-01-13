using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class DescriptionTest
    {
        [Test]
        public void EmptyNameTest()
        {
            var var1 =  IDescriptionFactory.Default.Create( "" );
            var var2 =  IDescriptionFactory.Default.Create( default! );

            Assert.AreEqual( string.Empty, var1.Value );
            Assert.AreEqual( string.Empty, var2.Value );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = IDescriptionFactory.Default.Create( "Hoge" );
            var huga = IDescriptionFactory.Default.Create( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = IDescriptionFactory.Default.Create( "Hoge" );
            var hoge2 = IDescriptionFactory.Default.Create( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IDescriptionFactory.Default.Create( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( IDescriptionFactory.Default.Create( "Hoge" ).ToString() == "Hoge" );
        }

    }
}