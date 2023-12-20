using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Linq;

namespace BookigApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructor()
        {
            Hotel hotel = new("Hotel", 5);
            Assert.AreEqual("Hotel", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
            Assert.IsNotNull(hotel.Bookings);
            Assert.IsNotNull(hotel.Rooms);
        }
        [Test]
        public void Test_FullName_ThorwsException()
        {
            Hotel hotel;
            Assert.Throws<ArgumentNullException>(() =>  hotel = new Hotel(null, 5));
        }
        [Test]
        public void Test_FullName_CategoryThorwsExceptionOver5()
        {
            Hotel hotel;
            Assert.Throws<ArgumentException>(() => hotel = new Hotel("Hotel", 6));
        }
        [Test]
        public void Test_FullName_CategoryThorwsExceptionUnder1()
        {
            Hotel hotel;
            Assert.Throws<ArgumentException>(() => hotel = new Hotel("Hotel", 0));
        }

        [Test]
        public void Test_AddRoomWorkProperly()
        {
            Hotel hotel = new("Hotel", 5);
            Room room = new(2, 130);
            hotel.AddRoom(room);
            Assert.AreEqual(1, hotel.Rooms.Count);
            Assert.IsNotNull(2, hotel.Rooms.First().BedCapacity.ToString());
        }
        [Test]
        public void Test_Booking_ThrowsException_Under1Adult()
        {
            Hotel hotel = new("Hotel", 5);
            Room room = new(10, 130);
            hotel.AddRoom(room);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(0, 2, 3, 300));
        }
        [Test]
        public void Test_Booking_ThrowsException_ChildrenUnder1()
        {
            Hotel hotel = new("Hotel", 5);
            Room room = new(10, 130);
            hotel.AddRoom(room);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, -3, 3, 300));
        }
        [Test]
        public void Test_Booking_ThrowsException_ResidenceDurationUnder1()
        {
            Hotel hotel = new("Hotel", 5);
            Room room = new(10, 130);
            hotel.AddRoom(room);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2,2, 0, 300));
        }
        [Test]
        public void Test_Booking_WorkProperly ()
        {
            Hotel hotel = new("Hotel", 5);
            Room room = new(10, 50);
            hotel.AddRoom(room);
            hotel.BookRoom(2, 2, 2, 300);
            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(100, hotel.Turnover);
            Assert.AreEqual(hotel.Bookings.First().BookingNumber, 1);
        }
        [Test]
        public void Test_Booking_CannotGiveBooking_BecauseBedCapacity()
        {
            Hotel hotel = new("Hotel", 5);
            Room room = new(10, 50);
            hotel.AddRoom(room);
            hotel.BookRoom(10, 6, 2, 5_000);
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Turnover);
            Assert.AreEqual(hotel.Bookings.Count, 0);
        }
        [Test]
        public void Test_Booking_CannotGiveBooking()
        {
            Hotel hotel = new("Hotel", 5);
            Room room = new(20, 75);
            hotel.AddRoom(room);
            hotel.BookRoom(10, 6, 10, 500);
            Assert.AreEqual(0, hotel.Bookings.Count);
            Assert.AreEqual(0, hotel.Turnover);
            Assert.AreEqual(hotel.Bookings.Count, 0);
        }


    }
}