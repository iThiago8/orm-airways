using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Controllers
{
	public class AircraftController : Controller
	{
		private readonly IAircraftRepository _aircraftRepository;

		
		public IActionResult Index()
		{
			return View();
		}
	}
}
