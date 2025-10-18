using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Controllers
{
    public class CustomerController(ICustomerRepository customerRepository) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await customerRepository.GetAll();

            if (customers == null)
                return View();

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            await customerRepository.Create(customer);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var customer = await customerRepository.GetById(id);
            if (customer == null)
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Customer customer)
        {
            if (!ModelState.IsValid)
                return View(customer);

            await customerRepository.Update(customer);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await customerRepository.GetById(id);
            if (customer == null)
                return NotFound();

            await customerRepository.Delete(customer);
            return RedirectToAction("Index");

        }
    }
}
