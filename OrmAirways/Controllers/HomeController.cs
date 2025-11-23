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
        IFlightRepository flightRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var aircrafts = await aircraftRepository.GetAll();
            var airports = await airportRepository.GetAll();
            var customers = await customerRepository.GetAll();
            var bookings = await bookingRepository.GetAll();
            var flights = await flightRepository.GetAll();

            var viewModel = new DashboardViewModel
            {
                TotalAircrafts = aircrafts?.Count ?? 0,
                TotalAirports = airports?.Count ?? 0,
                TotalCustomers = customers?.Count ?? 0,
                TotalBookings = bookings?.Count ?? 0,
                UpcomingFlights = flights?
                    .Where(f => f.DepartureTime >= DateTime.Now)
                    .OrderBy(f => f.DepartureTime)
                    .Take(5)
                    .ToList() ??[]
            };

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