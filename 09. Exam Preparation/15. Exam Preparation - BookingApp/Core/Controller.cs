using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private HotelRepository hotels;

        public Controller()
        {
            hotels = new HotelRepository();
        }

        public string AddHotel(string hotelName, int category)
        {
            IHotel hadThatHotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);
            if (hadThatHotel != null)
            {
                return $"Hotel {hotelName} is already registered in our platform.";
            }

            IHotel hotel = new Hotel(hotelName, category);
            hotels.AddNew(hotel);

            return $"{category} stars hotel {hotelName} is registered in our platform and expects room availability to be uploaded.";
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (hotels.All().FirstOrDefault(h => h.Category == category) == null)
            {
                return $"{category} star hotel is not available in our platform.";
            }

            var orderedHotels = 
                hotels.All().Where(h => h.Category == category).OrderBy(x => x.Turnover).ThenBy(h => h.FullName);
            foreach (var hotel in orderedHotels)
            {
                IRoom selectedRoom = 
                    hotel.Rooms.All()
                    .Where(r => r.PricePerNight > 0)
                    .Where(b => b.BedCapacity >= adults + children)
                    .OrderBy(b => b.BedCapacity)
                    .FirstOrDefault();

                if (selectedRoom != null)
                {
                    int bookingNumber = this.hotels.All().Sum(x => x.Bookings.All().Count) + 1;
                    IBooking booking = new Booking(selectedRoom, duration, adults, children, bookingNumber);
                    hotel.Bookings.AddNew(booking);
                    return $"Booking number {booking.BookingNumber} for {hotel.FullName} hotel is successful!";
                }
            }
            return "We cannot offer appropriate room for your request.";
        }

        public string HotelReport(string hotelName)
        {
            StringBuilder sb = new StringBuilder();
            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);
            if (hotel == null) 
            {
                sb.AppendLine($"Profile {hotelName} doesn't exist!");
                return sb.ToString().Trim();
            }
            sb.AppendLine($"Hotel name: {hotel.FullName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:F2} $");
            sb.AppendLine("--Bookings:");
            if (hotel.Bookings.All().Count == 0)
            {
                sb.AppendLine("none");
            }
            else
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    sb.AppendLine($"{booking.BookingSummary()}");
                    sb.AppendLine();
                }
            }
            return sb.ToString().Trim();
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);
            if (hotel == null)
            {
                return $"Profile {hotelName} doesn’t exist!";
            }
            if (hotel.Rooms.Select(roomTypeName) == null)
            {
                return "Room type is not created yet!";
            }
            if (roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio) && roomTypeName != nameof(Apartment))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }
            IRoom room = hotel.Rooms.All().FirstOrDefault(r => r.GetType().Name == roomTypeName);
            if (room.PricePerNight != 0)
            {
                return "Price is already set!";
            }
            room.SetPrice(price);
            return $"Price of {roomTypeName} room type in {hotelName} hotel is set!";
        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);
            if (hotel == null)
            {
                return $"Profile {hotelName} doesn’t exist!";
            }
            if (hotel.Rooms.All().FirstOrDefault(r => r.GetType().Name == roomTypeName) != null)
            {
                return $"Room type is already created!";
            }
            if (roomTypeName != nameof(Apartment) && roomTypeName != nameof(DoubleBed) && roomTypeName != nameof(Studio))
            {
                throw new ArgumentException("Incorrect room type!");
            }
            IRoom room = null;
            if (roomTypeName == nameof(Apartment))
            {
                room = new Apartment();
            }
            else if (roomTypeName == nameof(DoubleBed))
            {
                room = new DoubleBed();
            }
            else if (roomTypeName == nameof(Studio))
            {
                room = new Studio();
            }
            hotel.Rooms.AddNew(room);
            return $"Successfully added {roomTypeName} room type in {hotelName} hotel!";
        }
    }
}
