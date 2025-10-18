using OrmAirways.Models;
using OrmAirways.Data;
using OrmAirways.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace OrmAirways.Controllers
{
    public class AirportController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAirportRepository _context;

        public AirportController(ILogger<HomeController> logger, IAirportRepository airportRepository)
        {
            _context = airportRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Airport airport)
        {
            if(ModelState.IsValid)
            {
                await _context.Create(airport);
                return RedirectToAction("Index");
            }

            return View(airport);
        }
    }
}
