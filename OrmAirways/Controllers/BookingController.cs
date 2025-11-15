using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (!ModelState.IsValid)
                return View(booking);

            await bookingRepository.Create(booking);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var booking = await bookingRepository.GetById(id);
            if (booking == null)
                return NotFound();

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Booking booking)
        {
            if (!ModelState.IsValid)
                return View(booking);

            await bookingRepository.Update(booking);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await bookingRepository.GetById(id);
            if (booking == null)
                return NotFound();

            await bookingRepository.Delete(booking);
            return RedirectToAction("Index");
        }
    }
}
