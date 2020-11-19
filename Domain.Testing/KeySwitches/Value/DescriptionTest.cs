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
            var var1 =  new Description( "" );
            var var2 =  new Description( default! );

            Assert.AreEqual( string.Empty, var1.Value );
            Assert.AreEqual( string.Empty, var2.Value );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = new Description( "Hoge" );
            var huga = new Description( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = new Description( "Hoge" );
            var hoge2 = new Description( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new Description( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( new Description( "Hoge" ).ToString() == "Hoge" );
        }

    }
}