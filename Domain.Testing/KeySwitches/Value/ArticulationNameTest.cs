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
            var hoge = IArticulationNameFactory.Default.Create( "Hoge" );
            var huga = IArticulationNameFactory.Default.Create( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = IArticulationNameFactory.Default.Create( "Hoge" );
            var hoge2 = IArticulationNameFactory.Default.Create( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( IArticulationNameFactory.Default.Create( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( IArticulationNameFactory.Default.Create( "Hoge" ).ToString() == "Hoge" );
        }

    }
}