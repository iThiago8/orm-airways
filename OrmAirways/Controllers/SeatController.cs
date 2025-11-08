using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;
using System.Diagnostics;

namespace OrmAirways.Controllers
{
	public class SeatController : Controller
	{
		private readonly ISeatRepository _seatRepository;

		public SeatController(ISeatRepository seatRepository)
		{
			_seatRepository = seatRepository;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _seatRepository.GetAll());
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Seat seat)
		{
			if(ModelState.IsValid)
			{
				await _seatRepository.Create(seat);
				return RedirectToAction("Index");
			}
			return View(seat);
		}

		[HttpGet]
		public async Task<IActionResult> Update (int? id)
		{
			var seat = await _seatRepository.GetById(id.Value);
			if (seat == null)
			{
				return NotFound();
			}
			if (!id.HasValue)
			{
				return BadRequest();
			}
			return View(seat);
		}

		[HttpPost]
		public async Task<IActionResult> Update (int? id, Seat seat)
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}
			if (ModelState.IsValid)
			{
				await _seatRepository.Update(seat);
				return RedirectToAction("Index");
			}
			return View(seat);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int id)
		{
			var seat = await _seatRepository.GetById(id);
			if (seat == null)
			{
				return NotFound();
			}
			await _seatRepository.Delete(seat);
			return RedirectToAction("Index");
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