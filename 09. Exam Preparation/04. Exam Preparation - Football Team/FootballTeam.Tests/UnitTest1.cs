using NUnit.Framework;
using System;

namespace FootballTeam.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            FootballTeam footballTeam = null;
        }

        [Test]
        public void TestConstructorWork()
        {
            FootballTeam footballTeam = new FootballTeam("Tezt", 22);
            Assert.AreEqual(footballTeam.Capacity, 22);
            Assert.AreEqual(footballTeam.Name, "Tezt");
        }
        [Test]
        public void TestInvalidNameTrowsException()
        {
            FootballTeam footballTeam;
            Assert.Throws<ArgumentException>(() => footballTeam = new FootballTeam(null, 22));
        }
        [Test]
        public void TestInvalidCapacityTrowsException()
        {
            FootballTeam footballTeam;
            Assert.Throws<ArgumentException>(() => footballTeam = new FootballTeam("Tezt", 10));
        }
        [Test]
        public void TestCannotAddMoreThan15TrowsException()
        {
            FootballTeam footballTeam = new FootballTeam("sdf", 15);
            for (int i = 0; i < 15; i++)
            {
                FootballPlayer footballPlayer = new FootballPlayer($"{i}", i + 1, "Midfielder");
                footballTeam.AddNewPlayer(footballPlayer);
            }
            FootballPlayer footballPlayerException = new FootballPlayer("This", 16, "Midfielder");
            footballTeam.AddNewPlayer(footballPlayerException);
            Assert.AreEqual("No more positions available!", footballTeam.AddNewPlayer(footballPlayerException));
        }
        [Test]
        public void TestCannotAddMCorrexctly()
        {
            FootballTeam footballTeam = new FootballTeam("sdf", 15);
            FootballPlayer footballPlayerException = new FootballPlayer("This", 1, "Midfielder");
            footballTeam.AddNewPlayer(footballPlayerException);
            Assert.AreEqual("Added player This in position Midfielder with number 1", footballTeam.AddNewPlayer(footballPlayerException));
        }
        [Test]
        public void TestPlayerScoreMethodWorkCorrectly()
        {
            FootballTeam footballTeam = new FootballTeam("sdf", 15);
            FootballPlayer footballPlayer = new FootballPlayer("This", 1, "Midfielder");
            footballTeam.AddNewPlayer(footballPlayer);
            Assert.AreEqual("This scored and now has 1 for this season!", footballTeam.PlayerScore(1));
        }

    }
}