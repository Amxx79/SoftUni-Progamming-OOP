using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AliveDummyCannotGiveXp
    {
        [Test]
        public void AliveDummy_CannotGive_Expirience() 
        {
            Dummy dumm = new(100, 100);

            dumm.TakeAttack(99);
            Assert.Throws<InvalidOperationException>(
            () => dumm.GiveExperience(),
                "Target is not dead.");
        }
    }
}
