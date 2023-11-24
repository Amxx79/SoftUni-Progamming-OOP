using NUnit.Framework;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void DummyLoses_HealthIf_Attacked()
        {
            Axe ax = new(1, 10);
            Dummy dumm = new(10, 10);

            ax.Attack(dumm);
            Assert.That(dumm.Health, Is.EqualTo(9));
        }
    }
}