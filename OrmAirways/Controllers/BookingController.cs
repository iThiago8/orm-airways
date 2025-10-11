using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.ViewModels.Booking;

namespace OrmAirways.Controllers
{
    public class BookingController(IBookingRepository bookingRepository) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var bookings = await bookingRepository.GetAll();

            if (bookings == null)
                return View();

            ICollection<BookingViewModel> viewModels = [];

            foreach (var b in bookings)
            {
                viewModels.Add(new BookingViewModel
                {
                    ID = b.ID,
                    Customer = b.Customer,
                    CustomerID = b.CustomerID,
                    Flight = b.Flight,
                    FlightID = b.FlightID,
                    Seat = b.Seat,
                    SeatID = b.SeatID
                });
            }

            return View(viewModels);
        }
    }
}
