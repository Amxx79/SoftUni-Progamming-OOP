namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ArenaTests
    {
        [Test]
        public void ArenaCreate_TestIsCreatedProperly()
        {
            Arena arena = new Arena();
            Assert.IsNotNull(arena);
        }
        [Test]
        public void Arena_Test_WarriorPropertyGetterTest_IsReturnCorrectly()
        {
            Arena arena = new Arena();
            Warrior warrior = new("Horhe", 50, 50);
            arena.Enroll(warrior);
            Assert.IsNotNull(arena.Warriors);
        }
        [Test]
        public void Arena_Test_WarriorPropertyGetCountTest_IsReturnCorrectly()
        {
            Arena arena = new Arena();
            Warrior warrior = new("Horhe", 50, 50);
            arena.Enroll(warrior);
            int expectedResult = 1;
            Assert.AreEqual(expectedResult, arena.Count);
        }
        [Test]
        public void Arena_Test_ThrowExceptionIfWarriorIsInArena()
        {
            Arena arena = new Arena();
            Warrior warrior = new("Horhe", 50, 50);
            arena.Enroll(warrior);
            Assert.Throws<InvalidOperationException>(() 
                => arena.Enroll(warrior), "Warrior is already enrolled for the fights!");
        }
        [Test]
        public void ArenaTest_CheckFightMethodWarriorsIsNotNull()
        {
            Arena arena = new Arena();
            Warrior warrior = new("Horhe", 50, 50);
            Warrior warrior2 = new("Porhe", 50, 50);
            arena.Enroll(warrior);
            arena.Enroll(warrior2);
            int expectedHP1 = 50;
            int expectedHP2 = 50;

            Assert.AreEqual(expectedHP1, warrior.HP);
            Assert.AreEqual(expectedHP2, warrior2.HP);
        }
        [Test]
        public void ArenaTest_CheckFightMethod()
        {
            Arena arena = new Arena();
            Warrior warrior = new("Horhe", 50, 20);
            Warrior warrior2 = new("Porhe", 50, 50);
            arena.Enroll(warrior);
            arena.Enroll(warrior2);
            Assert.Throws<InvalidOperationException>(() => arena.Fight(warrior.Name, warrior2.Name));
        }
        [Test]
        public void ArenaTest_CheckFightMethod_IfAtackerWarriorIsNull()
        {
            Arena arena = new Arena();
            Warrior warrior = new("Horhe", 50, 20);
            Warrior warrior2 = new("Porhe", 50, 50);
            arena.Enroll(warrior);
            arena.Enroll(warrior2);
            Assert.Throws<InvalidOperationException>(() => arena.Fight("Alexandro", warrior2.Name), "There is no fighter with name Alexandro enrolled for the fights!");
        }
        [Test]
        public void ArenaTest_CheckFightMethod_IfDefenderWarriorIsNull()
        {
            Arena arena = new Arena();
            Warrior warrior = new("Horhe", 50, 20);
            Warrior warrior2 = new("Porhe", 50, 50);
            arena.Enroll(warrior);
            arena.Enroll(warrior2);
            Assert.Throws<InvalidOperationException>(() => arena.Fight(warrior.Name, "Jorjiano"), "There is no fighter with name Jorjiano enrolled for the fights!");
        }


    }
}
