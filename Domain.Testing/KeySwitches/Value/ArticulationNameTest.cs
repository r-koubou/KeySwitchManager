using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Domain.KeySwitches.Value;

using NUnit.Framework;

namespace Domain.Testing.KeySwitches.Value
{
    [TestFixture]
    public class ArticulationNameTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<InvalidNameException>( () =>  IArticulationNameFactory.Default.Create( "" ) );
            Assert.Throws<InvalidNameException>( () =>  IArticulationNameFactory.Default.Create( "  " ) );
            _ = IArticulationNameFactory.Default.Create( "Hoge" );
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