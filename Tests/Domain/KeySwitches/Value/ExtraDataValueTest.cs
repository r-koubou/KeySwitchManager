using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

using RkHelper.Text;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class ExtraDataValueTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<EmptyTextException>( () => _ = new ExtraDataValue( "" ) );
            Assert.Throws<EmptyTextException>( () => _ = new ExtraDataValue( "  " ) );
            _ = new ExtraDataValue( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = new ExtraDataValue( "Hoge" );
            var huga = new ExtraDataValue( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = new ExtraDataValue( "Hoge" );
            var hoge2 = new ExtraDataValue( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new ExtraDataValue( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( new ExtraDataValue( "Hoge" ).ToString() == "Hoge" );
        }

    }
}