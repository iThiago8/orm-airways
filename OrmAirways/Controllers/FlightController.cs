using Microsoft.AspNetCore.Mvc;

namespace OrmAirways.Controllers
{
	public class FlightController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
