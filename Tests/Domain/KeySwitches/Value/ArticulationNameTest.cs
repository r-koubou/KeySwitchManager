using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

using RkHelper.Text;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class ArticulationNameTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<EmptyTextException>( () => _ = new ArticulationName( "" ) );
            Assert.Throws<EmptyTextException>( () => _ = new ArticulationName( "  " ) );
            _ = new ArticulationName( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = new ArticulationName( "Hoge" );
            var huga = new ArticulationName( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = new ArticulationName( "Hoge" );
            var hoge2 = new ArticulationName( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new ArticulationName( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( new ArticulationName( "Hoge" ).ToString() == "Hoge" );
        }

    }
}