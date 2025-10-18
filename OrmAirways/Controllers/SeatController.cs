using Microsoft.AspNetCore.Mvc;

namespace OrmAirways.Controllers
{
	public class SeatController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
