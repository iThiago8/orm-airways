using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;
using System.Diagnostics;

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
