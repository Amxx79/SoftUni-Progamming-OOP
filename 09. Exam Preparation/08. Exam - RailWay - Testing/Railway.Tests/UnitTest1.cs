namespace Railway.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestPlatform.ObjectModel;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConsttructor()
        {
            RailwayStation rail = new RailwayStation("this");

            Assert.AreEqual(rail.Name, "this");
            Assert.IsNotNull(rail.ArrivalTrains);
            Assert.IsNotNull(rail.ArrivalTrains);
            Assert.IsNotNull(rail.DepartureTrains);
        }

        [Test]
        public void TestName()
        {
            RailwayStation rail;
            Assert.Throws<ArgumentException>(() => rail = new RailwayStation(null));
        }
        [Test]
        public void NewArrivalOnBoardMethod()
        {
            RailwayStation rail = new RailwayStation("Train");
            rail.NewArrivalOnBoard("Arival");
            Assert.AreEqual(rail.ArrivalTrains.Count, 1);

        }
        [Test]
        public void TrainHasArrivedMethod()
        {
            RailwayStation rail = new RailwayStation("Train");
            rail.NewArrivalOnBoard("Arival");
            Assert.AreEqual(rail.TrainHasArrived("AnotherArival"), "There are other trains to arrive before AnotherArival.");
        }
        [Test]
        public void TrainHasArrived()
        {
            RailwayStation rail = new RailwayStation("Train");
            rail.NewArrivalOnBoard("Arival");
            Assert.AreEqual(rail.TrainHasArrived("Arival"), "Arival is on the platform and will leave in 5 minutes.");
            Assert.AreEqual(rail.DepartureTrains.Count, 1);
            Assert.AreEqual(rail.ArrivalTrains.Count, 0);
        }
        [Test]
        public void TrainhasleftMethod()
        {
            RailwayStation rail = new RailwayStation("Train");
            rail.NewArrivalOnBoard("Arival");
            rail.TrainHasArrived("Arival");
            Assert.AreEqual(true, rail.TrainHasLeft("Arival"));
            Assert.AreEqual(rail.DepartureTrains.Count, 0);
        }
        [Test]
        public void TrainhasleftMethoddsg()
        {
            RailwayStation rail = new RailwayStation("Train");
            rail.NewArrivalOnBoard("Arival");
            rail.TrainHasArrived("Arival");
            Assert.AreEqual(false, rail.TrainHasLeft("AnotherArival"));
            Assert.AreEqual(rail.DepartureTrains.Count, 1);
        }
        [Test]
        public void NameTester()
        {
            RailwayStation rail = new RailwayStation("Train");
            Assert.AreEqual(rail.Name, "Train");
        }

    }
}