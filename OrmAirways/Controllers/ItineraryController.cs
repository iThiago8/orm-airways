using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;

namespace OrmAirways.Controllers
{
	public class ItineraryController : Controller
	{
		private readonly IItineraryRepository _aircraftRepository;
		priva
		public IActionResult Index()
		{
			return View();
		}
	}
}
