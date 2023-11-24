using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DeadDummyGiveXp
    {
        [Test]
        public void DeadDummyCan_GiveXp()
        {
            Dummy dummy = new Dummy(100, 100);

            //Act
            dummy.TakeAttack(100);

            //Assert
            Assert.AreEqual(100, dummy.GiveExperience());
        }

    }
}
