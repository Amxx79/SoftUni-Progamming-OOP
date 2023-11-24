using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DeadDummyException
    {
        [Test]
        public void ThrowExceptionIf_DummyIsDead()
        {
            Axe ax = new(10, 10);
            Dummy dumm = new(1, 10);

            ax.Attack(dumm);
            Assert.Throws<InvalidOperationException>(() => ax.Attack(dumm));
        }
    }
}
