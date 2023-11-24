using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.Tests
{
    [TestFixture]
    public class GunIsBrokenTests
    {
        [Test]
        public void AxeDurability_IsUnderZero_GunIsBroken()
        {
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(10, 10);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
        }
    }
}
