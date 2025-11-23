using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Controllers
{
    public class CustomerController(ICustomerRepository customerRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await customerRepository.GetAll());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (!id.HasValue) 
                return BadRequest();

            var customer = await customerRepository.GetById(id.Value);

            if (customer == null) 
                return NotFound();
            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (!ModelState.IsValid) 
                return View(customer);

            try
            {
                if (customer.Id == Guid.Empty) customer.Id = Guid.NewGuid();

                // Regra de Negócio: Validar se CPF já existe

                await customerRepository.Create(customer);

                TempData["SuccessMessage"] = "Cliente cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao cadastrar: " + ex.Message);
                return View(customer);
            }
        }

        public async Task<IActionResult> Update(Guid? id)
        {
            if (!id.HasValue) 

                return BadRequest();
            var customer = await customerRepository.GetById(id.Value);

            if (customer == null) 
                return NotFound();

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, Customer customer)
        {
            if (id != customer.Id) 
                return BadRequest();

            if (!ModelState.IsValid) 
                return View(customer);

            try
            {
                await customerRepository.Update(customer);
                TempData["SuccessMessage"] = "Dados do cliente atualizados!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Erro ao atualizar dados.");
                return View(customer);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var customer = await customerRepository.GetById(id);
            if (customer == null) 
                return NotFound();

            try
            {
                await customerRepository.Delete(customer);
                TempData["SuccessMessage"] = "Cliente removido.";
            }
            catch
            {
                TempData["ErrorMessage"] = "Não é possível remover este cliente pois ele possui reservas ativas.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}