using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.ViewModels.Customer;
using OrmAirways.Models;
using System.Net.WebSockets;

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

            ICollection<CustomerViewModel> viewModels = [];

            foreach (var c in customers)
            {
                viewModels.Add(new CustomerViewModel
                {
                    ID = c.ID,
                    CPF = c.CPF,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber,
                    IsVIP = c.IsVIP
                });
            }

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            await customerRepository.Create(new Customer
            {
                CPF = viewModel.CPF,
                Name = viewModel.Name,
                PhoneNumber = viewModel.PhoneNumber
            });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var customer = await customerRepository.GetById(id);
            if (customer == null)
                return NotFound();

            return View(new UpdateCustomerViewModel
            {
                CPF = customer.CPF,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                IsVIP = customer.IsVIP
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            await customerRepository.Update(new Customer
            {
                ID = viewModel.ID,
                CPF = viewModel.CPF,
                Name = viewModel.Name,
                PhoneNumber = viewModel.PhoneNumber,
                IsVIP = viewModel.IsVIP
            });

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
