using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

using RkHelper.Text;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class DeveloperNameTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<EmptyTextException>( () => IDeveloperNameFactory.Default.Create( "" ) );
            Assert.Throws<EmptyTextException>( () => IDeveloperNameFactory.Default.Create( "  " ) );
            _ = IDeveloperNameFactory.Default.Create( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = IDeveloperNameFactory.Default.Create( "Hoge" );
            var huga = IDeveloperNameFactory.Default.Create( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = IDeveloperNameFactory.Default.Create( "Hoge" );
            var hoge2 = IDeveloperNameFactory.Default.Create( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IDeveloperNameFactory.Default.Create( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( IDeveloperNameFactory.Default.Create( "Hoge" ).ToString() == "Hoge" );
        }

    }
}