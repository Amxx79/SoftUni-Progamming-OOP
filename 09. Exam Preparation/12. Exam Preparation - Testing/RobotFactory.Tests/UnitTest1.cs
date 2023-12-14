using NUnit.Framework;
using System.Linq;

namespace RobotFactory.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCreationNewFactory()
        {
            Factory factory = new("Factory", 5);
            Assert.AreEqual(factory.Capacity, 5);
            Assert.AreEqual(factory.Name, "Factory");
            Assert.IsNotNull(factory.Robots);
            Assert.IsNotNull(factory.Supplements);
        }
        [Test]
        public void TestMethod_ProduceRobot()
        {
            Factory factory = new("Factory", 5);
            Assert.AreEqual("Produced --> Robot model: Robot IS: 10045, Price: 100.00", factory.ProduceRobot("Robot", 100, 10045));
            Assert.AreEqual("Produced --> Robot model: Robot2 IS: 10045, Price: 1000.00", factory.ProduceRobot("Robot2", 1000, 10045));
            Assert.AreEqual("Produced --> Robot model: Robot3 IS: 10045, Price: 10000.00", factory.ProduceRobot("Robot3", 10000, 10045));
            Assert.AreEqual("Produced --> Robot model: Robot4 IS: 10045, Price: 100000.00", factory.ProduceRobot("Robot4", 100000, 10045));
            Assert.AreEqual(factory.Robots.Count, 4);
        }
        [Test]
        public void TestMethod_ProduceRobot_CannotAdd_CapacityLimits()
        {
            Factory factory = new("Factory", 1);
            Assert.AreEqual("Produced --> Robot model: Robot IS: 10045, Price: 100.00", factory.ProduceRobot("Robot", 100, 10045));
            Assert.AreEqual(factory.ProduceRobot("SecondRobot", 100, 10045), "The factory is unable to produce more robots for this production day!");
        }
        [Test]
        public void TestMethod_ProduceSupplement_AddCorrectly()
        {
            Factory factory = new("Factory", 5);
            Assert.AreEqual("Produced --> Robot model: Robot IS: 10045, Price: 100.00", factory.ProduceRobot("Robot", 100, 10045));
            Assert.AreEqual(factory.ProduceSupplement("FirstSupplement", 10045), "Supplement: FirstSupplement IS: 10045");
            Assert.AreEqual(1, factory.Supplements.Count);
        }
        [Test]
        public void TestMethod_UpgradeRobot_AddCorrectly()
        {
            Factory factory = new("Factory", 5);
            Robot robot = new("Robot", 100, 10000);
            factory.ProduceRobot(robot.Model, robot.Price, robot.InterfaceStandard);
            Supplement supplement = new("NewOne", 10000);
            Assert.AreEqual(true, factory.UpgradeRobot(robot, supplement));
        }
        [Test]
        public void TestMethod_UpgradeRobot_CannotUpgradeIt()
        {
            Factory factory = new("Factory", 5);
            Robot robot = new("Robot", 100, 10000);
            factory.ProduceRobot(robot.Model, robot.Price, robot.InterfaceStandard);
            Supplement supplement = new("NewOne", 10045);
            Assert.AreEqual(false, factory.UpgradeRobot(robot, supplement));
        }
        [Test]
        public void TestMethod_UpgradeRobot_ReturnFalse()
        {
            Factory factory = new("Factory", 5);
            Robot robot = new("Robot", 100, 10000);
            factory.ProduceRobot(robot.Model, robot.Price, robot.InterfaceStandard);
            Supplement supplement = new("NewOne", 10000);
            factory.UpgradeRobot(robot, supplement);
            Assert.AreEqual(false, factory.UpgradeRobot(robot, supplement));
        }
        [Test]
        public void TestMethod_SellRobot_ReturnCorrectly()
        {
            Factory factory = new("Factory", 5);
            Robot robot = new("Robot", 1000, 10000);
            factory.ProduceRobot(robot.Model, robot.Price, robot.InterfaceStandard);
            Robot first = factory.Robots.FirstOrDefault(r => r.Price == 1000);
            Robot second = factory.SellRobot(1300);
            Assert.AreSame(first, second);
        }
    }
}