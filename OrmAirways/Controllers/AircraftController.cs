using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Controllers
{
	public class AircraftController : Controller
	{
		private readonly IAircraftRepository _aircraftRepository;

		public AircraftController(IAircraftRepository aircraftRepository)
		{
			_aircraftRepository = aircraftRepository; //Constructor Injection; Construtor feito
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _aircraftRepository.GetAll());
		}

		[HttpPost]
		public async Task<IActionResult> Create(Aircraft aircraft)
		{
			if (ModelState.IsValid)
			{
				await _aircraftRepository.Create(aircraft);
				return RedirectToAction("Index");
			}

			return View(aircraft);
		}

		[HttpPost]
		public async Task<IActionResult> Update(int? id, Aircraft aircraft)
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}
			if (ModelState.IsValid)
			{
				await _aircraftRepository.Update(aircraft);
				return RedirectToAction("Index");
			}
			return View(aircraft);
		}

		[HttpGet]
		public async Task<IActionResult> Update(int? id)
		{
			var aircraft = await _aircraftRepository.GetById(id.Value);
			if (aircraft == null) // = símbolo de atribuição -> == símbolo de comparação.
			{
				return NotFound();
			}
			if (!id.HasValue)
			{
				return BadRequest();
			}
			return View(aircraft);
		}
	}
}
