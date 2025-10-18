using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;

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

            return View(bookings);
        }
    }
}
