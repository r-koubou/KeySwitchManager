using ArticulationManager.Common.Utilities;
using ArticulationManager.Domain.Articulations.Value;

using NUnit.Framework;

namespace Domain.Testing.Articulations.Value
{
    [TestFixture]
    public class DeveloperNameTest
    {
        [Test]
        public void EmptyNameTest()
        {
            Assert.Throws<InvalidNameException>( () =>  new DeveloperName( "" ) );
            Assert.Throws<InvalidNameException>( () =>  new DeveloperName( "  " ) );
            new DeveloperName( "Hoge" );
        }

        [Test]
        public void EqualityTest()
        {
            var hoge = new DeveloperName( "Hoge" );
            var huga = new DeveloperName( "Huga" );
            Assert.IsFalse( hoge.Equals( huga ) );

            var hoge1 = new DeveloperName( "Hoge" );
            var hoge2 = new DeveloperName( "Hoge" );
            Assert.IsTrue( hoge1.Equals( hoge2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new DeveloperName( "Hoge" ).ToString(), "Hoge" );
            Assert.IsTrue( new DeveloperName( "Hoge" ).ToString() == "Hoge" );
        }

    }
}