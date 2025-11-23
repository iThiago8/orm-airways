using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;
using OrmAirways.Models.ViewModels;
using System.Diagnostics;

namespace OrmAirways.Controllers
{
    public class HomeController(
        IAircraftRepository aircraftRepository,
        IAirportRepository airportRepository,
        ICustomerRepository customerRepository,
        IBookingRepository bookingRepository,
        IFlightRepository flightRepository,
        UserManager<IdentityUser> userManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var airports = await airportRepository.GetAll() ?? [];
            var bookings = await bookingRepository.GetAll() ?? [];
            var flights = await flightRepository.GetAll() ?? [];

            var viewModel = new DashboardViewModel();

            if (User.IsInRole("Admin"))
            {
                var aircrafts = await aircraftRepository.GetAll() ?? [];
                var customers = await customerRepository.GetAll() ?? [];

                viewModel.TotalAircrafts = aircrafts.Count;
                viewModel.TotalAirports = airports.Count;
                viewModel.TotalCustomers = customers.Count;
                viewModel.TotalBookings = bookings.Count;

                viewModel.UpcomingFlights = flights
                    .Where(f => f.DepartureTime >= DateTime.Now)
                    .OrderBy(f => f.DepartureTime)
                    .Take(5)
                    .ToList();
            }
            else
            {
                var userEmail = userManager.GetUserName(User);

                var myBookings = bookings.Where(b => b.Customer?.Email == userEmail).ToList();
                var myUpcomingBookings = myBookings
                    .Where(b => b.Flight != null && b.Flight.DepartureTime >= DateTime.Now)
                    .OrderBy(b => b.Flight!.DepartureTime)
                    .ToList();

                viewModel.TotalBookings = myBookings.Count;
                viewModel.TotalAirports = airports.Count;  

                if (myUpcomingBookings.Count > 0)
                {
                    viewModel.UpcomingFlights = myUpcomingBookings.Select(b => b.Flight!).Take(5).ToList();
                    ViewBag.ListTitle = "Seus Próximos Embarques";
                    ViewBag.ListStatus = "Confirmado";
                }
                else
                {
                    viewModel.UpcomingFlights = flights
                        .Where(f => f.DepartureTime >= DateTime.Now)
                        .OrderBy(f => f.DepartureTime)
                        .Take(5)
                        .ToList();
                    ViewBag.ListTitle = "Próximos Voos Disponíveis";
                    ViewBag.ListStatus = "Agendado";
                }
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}