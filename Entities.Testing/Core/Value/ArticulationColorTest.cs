using ArticulationManager.Entities.ArticulationData.Value;
using ArticulationManager.Utilities;

using NUnit.Framework;

namespace Entities.Testing.Core.Value
{
    [TestFixture]
    public class ArticulationColorTest
    {
        [Test]
        [TestCase( ArticulationColor.MinValue - 1 )]
        [TestCase( ArticulationColor.MaxValue + 1 )]
        public void OutOfRangeTest( int groupValue )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new ArticulationColor( groupValue ) );
        }

        [Test]
        public void EqualityTest()
        {
            var group1 = new ArticulationColor( 1 );
            var group2 = new ArticulationColor( 2 );
            Assert.IsTrue( group1.Equals( new ArticulationColor( 1 ) ) );
            Assert.IsFalse( group1.Equals( group2 ) );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new ArticulationColor( 1 ).ToString(),"1" );
            Assert.IsTrue( new ArticulationColor( 1 ).ToString() == "1" );
        }

    }
}