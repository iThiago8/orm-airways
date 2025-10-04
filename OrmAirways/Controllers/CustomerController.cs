using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;

namespace OrmAirways.Controllers
{
    public class CustomerController(ICustomerRepository customerRepository) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await customerRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
