using ArticulationManager.Entities.ArticulationData.Value;
using ArticulationManager.Utilities;

using NUnit.Framework;

namespace Entities.Testing.Core.Value
{
    [TestFixture]
    public class ArticulationIdTest
    {
        [Test]
        [TestCase( ArticulationId.MinValue - 1 )]
        [TestCase( ArticulationId.MaxValue + 1 )]
        public void OutOfRangeTest( int groupValue )
        {
            Assert.Throws<ValueOutOfRangeException>( () => new ArticulationId( groupValue ) );
        }

        [Test]
        public void EqualityTest()
        {
            var obj1 = new ArticulationId( ArticulationId.MinValue );
            var obj2 = new ArticulationId( ArticulationId.MaxValue );
            Assert.AreEqual( obj1, new ArticulationId( ArticulationId.MinValue ) );
            Assert.AreNotEqual( obj1, obj2 );
        }

        [Test]
        public void ToStringEqualityTest()
        {
            Assert.AreEqual( new ArticulationId( 1 ).ToString(),"1" );
        }

    }
}