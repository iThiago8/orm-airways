using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Controllers
{
    public class BookingController(
        IBookingRepository bookingRepository,
        IFlightRepository flightRepository,
        ICustomerRepository customerRepository,
        ISeatRepository seatRepository,
        UserManager<IdentityUser> userManager) : Controller
    {
        private async Task PopulateViewBags()
        {
            var flights = await flightRepository.GetAll() ?? [];
            var customers = await customerRepository.GetAll() ?? [];

            if (!User.IsInRole("Admin"))
            {
                var userEmail = userManager.GetUserName(User);
                var myCustomer = customers.FirstOrDefault(c => c.Email == userEmail);

                if (myCustomer != null)
                {
                    customers = [myCustomer];
                }
            }

            var flightItems = flights
                .Where(f => f.DepartureTime > DateTime.Now)
                .Select(f => new SelectListItem
                {
                    Value = f.Id.ToString(),
                    Text = $"{f.FlightNumber} | {f.Origin?.IataCode ?? "?"} > {f.Destination?.IataCode ?? "?"} ({f.DepartureTime:dd/MM HH:mm})"
                })
                .ToList();

            var customerItems = customers
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FullName} (CPF: {c.DocumentNumber})"
                })
                .ToList();

            ViewBag.FlightId = flightItems;
            ViewBag.CustomerId = customerItems;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await bookingRepository.GetAll() ?? [];

            if (User.IsInRole("Admin"))
            {
                return View(bookings);
            }

            var userEmail = userManager.GetUserName(User);
            var myBookings = bookings.Where(b => b.Customer?.Email == userEmail).ToList();
            return View(myBookings);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var booking = await bookingRepository.GetById(id.Value);

            if (booking == null)
                return NotFound();

            if (!User.IsInRole("Admin"))
            {
                var userEmail = userManager.GetUserName(User);
                if (booking.Customer?.Email != userEmail)
                {
                    return Forbid();
                }
            }

            return View(booking);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateViewBags();
            return View(new Booking());
        }

        [HttpGet]
        public async Task<IActionResult> GetSeatsForFlight(Guid flightId)
        {
            var flight = await flightRepository.GetById(flightId);

            if (flight == null)
                return NotFound();

            var allSeats = await seatRepository.GetAll() ?? [];

            var aircraftSeats = allSeats
                .Where(s => s.AircraftId == flight.AircraftId)
                .OrderBy(s => s.Code.Length).ThenBy(s => s.Code)
                .ToList();

            var bookings = await bookingRepository.GetAll() ?? [];

            var takenSeatIds = bookings
                .Where(b => b.FlightId == flightId && b.SeatId.HasValue)
                .Select(b => b.SeatId!.Value)
                .ToList();

            var seatMap = aircraftSeats.Select(s => new
            {
                s.Id,
                s.Code,
                Class = s.ClassType,
                IsTaken = takenSeatIds.Contains(s.Id)
            });

            return Json(seatMap);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            ModelState.Remove("Seat");
            ModelState.Remove("TicketCode");
            ModelState.Remove("Customer");
            ModelState.Remove("Flight");

            if (!User.IsInRole("Admin"))
            {
                var customers = await customerRepository.GetAll() ?? [];
                var userEmail = userManager.GetUserName(User);
                var myCustomer = customers.FirstOrDefault(c => c.Email == userEmail);

                if (myCustomer == null)
                {
                    ModelState.AddModelError("", "Seu usuário não possui cadastro de cliente vinculado.");
                    await PopulateViewBags();
                    return View(booking);
                }

                booking.CustomerId = myCustomer.Id;
            }

            if (!ModelState.IsValid)
            {
                await PopulateViewBags();
                return View(booking);
            }

            try
            {
                var existingBookings = await bookingRepository.GetAll() ?? [];
                bool isTaken = existingBookings.Any(b => b.FlightId == booking.FlightId && b.SeatId == booking.SeatId);

                if (isTaken)
                {
                    ModelState.AddModelError("", "Desculpe, este assento acabou de ser reservado por outra pessoa.");
                    await PopulateViewBags();
                    return View(booking);
                }

                booking.Id = Guid.NewGuid();
                booking.TicketCode = Guid.NewGuid().ToString()[..6].ToUpper();
                booking.BookingDate = DateTime.UtcNow;
                booking.IsConfirmed = true;

                await bookingRepository.Create(booking);

                TempData["SuccessMessage"] = "Reserva realizada com sucesso! Ticket: " + booking.TicketCode;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro: " + ex.Message);
                await PopulateViewBags();
                return View(booking);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var booking = await bookingRepository.GetById(id);
            if (booking != null)
            {
                if (!User.IsInRole("Admin"))
                {
                    var userEmail = userManager.GetUserName(User);
                    if (booking.Customer?.Email != userEmail) return Forbid();
                }

                if (booking.Flight != null && booking.Flight.DepartureTime < DateTime.Now)
                {
                    TempData["ErrorMessage"] = "Não é possível cancelar reservas de voos passados.";
                }
                else
                {
                    await bookingRepository.Delete(booking);
                    TempData["SuccessMessage"] = "Reserva cancelada e assento liberado.";
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}