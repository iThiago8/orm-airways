using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;
using System.Diagnostics;

namespace OrmAirways.Controllers
{
	public class AircraftController : Controller
	{
		private readonly IAircraftRepository _aircraftRepository;
		private readonly ISeatRepository _seatRepository;

		public AircraftController(IAircraftRepository aircraftRepository, ISeatRepository seatRepository)
		{
			_aircraftRepository = aircraftRepository; //Constructor Injection; Construtor feito
			_seatRepository = seatRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _aircraftRepository.GetAll());
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Aircraft aircraft)
		{
			if (ModelState.IsValid)
			{
				await _aircraftRepository.Create(aircraft);
				string[] layout = { "Janela", "Corredor", "Corredor", "Janela" };

				for (int i = 0; i < aircraft.SeatNumber; i++)
				{
					int location = i % layout.Length;

					Seat seat = new Seat()
					{
						Aircraft = aircraft,
						AircraftID = aircraft.ID,
						Location = layout[location]
					};

					await _seatRepository.Create(seat);

				}

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

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var aircraft = await _aircraftRepository.GetById(id);
			if(aircraft == null)
			{
				return NotFound();
			}
			await _aircraftRepository.Delete(aircraft);
			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}