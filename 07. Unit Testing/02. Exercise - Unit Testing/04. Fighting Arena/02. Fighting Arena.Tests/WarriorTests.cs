namespace FightingArena.Tests
{
    using Newtonsoft.Json.Linq;
    using NUnit.Framework;
    using System;
    using static System.Net.Mime.MediaTypeNames;

    [TestFixture]
    public class WarriorTests
    {
        private Warrior warrior;
        [Test]
        public void WarriorCreate_TestConstructor() 
        {
            warrior = new("Johny", 30, 100);
            string expectedName = "Johny";
            int expectedDamage = 30;
            int expectedHP = 100;

            Assert.AreEqual(expectedName, warrior.Name);
            Assert.AreEqual(expectedDamage, warrior.Damage);
            Assert.AreEqual(expectedHP, warrior.HP);
        }
        [Test]
        public void WarriorNameProperty_CheckIfThrowException_EmptyString()
        {
            Assert.Throws<ArgumentException>(()
                => warrior = new("", 30, 100), "Name should not be empty or whitespace");
        }
        [Test]
        public void Warrior_Damage_Property_CheckIfThrowException_EmptyString()
        {
            Assert.Throws<ArgumentException>(()
                => warrior = new("Johny", 0, 100), "Damage value should be positive!");
        }
        [Test]
        public void Warrior_HealthPoints_Property_CheckIfThrowException_EmptyString()
        {
            Assert.Throws<ArgumentException>(()
                => warrior = new("Johny", 30, -1), "HP should not be negative!");
        }
        [Test]
        public void Warrior_AttackMethod_CheckIfThrowException_IfHpIsLowerThan30()
        {
            warrior = new("Johny", 30, 50);
            Warrior methodWarrior = new("Alberto", 30, 10);

            Assert.Throws<InvalidOperationException>(()
                => warrior.Attack(methodWarrior), "Enemy HP must be greater than 30 in order to attack him!");
        }
        [Test]
        public void Warrior_AttackMethod_CheckIfThrowException_IfPrimaryWarriorHp_IsLowerThanOtherDamage()
        {
            warrior = new("Johny", 35, 40);
            Warrior methodWarrior = new("Alberto", 50, 50);

            Assert.Throws<InvalidOperationException>(()
                => warrior.Attack(methodWarrior), "You are trying to attack too strong enemy");
        }
        [Test]
        public void Warrior_AttackMethod_CheckIfWorkProperly()
        {
            warrior = new("Johny", 35, 60);
            Warrior methodWarrior = new("Alberto", 50, 40);
            int expectedWarriorHp = 10;
            warrior.Attack(methodWarrior);
            Assert.AreEqual(expectedWarriorHp, warrior.HP);
        }
        [Test]
        public void Warrior_AttackMethod_CheckIf_PrimaryDamage_IsBiggerThan_OtherHP()
        {
            warrior = new("Johny", 90, 60);
            Warrior methodWarrior = new("Alberto", 50, 50);
            warrior.Attack(methodWarrior);
            Assert.IsTrue(warrior.Damage > methodWarrior.HP);
        }
        [Test]
        public void Warrior_AttackMethod_CheckIf_WarriorHPSubstractProperly()
        {
            warrior = new("Johny", 50, 60);
            Warrior methodWarrior = new("Alberto", 50, 100);
            int expectedHP = 10;
            warrior.Attack(methodWarrior);
            Assert.AreEqual(expectedHP, warrior.HP);
        }

    }
}