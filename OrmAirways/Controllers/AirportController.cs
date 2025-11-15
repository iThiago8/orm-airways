using OrmAirways.Models;
using OrmAirways.Data;
using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;

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

        [HttpPost]
        public async  Task<IActionResult> Delete(Airport airport)
        {
            if (ModelState.IsValid)
            {
                await _context.Delete(airport);
                return RedirectToAction("Index");
            }

            return View(airport);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var airport = await _context.GetById(id!.Value);

            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Airport airport)
        {
            if (airport == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _context.Update(airport);
                return RedirectToAction("Index");
            }

            return View(airport);
        }
    }
}
