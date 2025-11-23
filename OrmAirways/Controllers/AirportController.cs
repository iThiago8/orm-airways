using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AirportController(IAirportRepository airportRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await airportRepository.GetAll());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (!id.HasValue) 
                return BadRequest();

            var airport = await airportRepository.GetById(id.Value);
            
            if (airport == null) 
                return NotFound();
            
            return View(airport);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Airport airport)
        {
            if (!ModelState.IsValid)
                return View(airport);

            try
            {
                if (airport.Id == Guid.Empty) airport.Id = Guid.NewGuid();

                await airportRepository.Create(airport);

                TempData["SuccessMessage"] = "Aeroporto cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(airport);
            }
        }

        public async Task<IActionResult> Update(Guid? id)
        {
            if (!id.HasValue) 
                return BadRequest();

            var airport = await airportRepository.GetById(id.Value);
            if (airport == null) 
                return NotFound();
            
            return View(airport);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, Airport airport)
        {
            if (id != airport.Id) 
                return BadRequest();
            
            if (!ModelState.IsValid) 
                return View(airport);

            try
            {
                await airportRepository.Update(airport);
                TempData["SuccessMessage"] = "Aeroporto atualizado!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Erro ao atualizar.");
                return View(airport);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var airport = await airportRepository.GetById(id);
            
            if (airport == null) 
                return NotFound();

            await airportRepository.Delete(airport);
            TempData["SuccessMessage"] = "Aeroporto removido.";
            return RedirectToAction(nameof(Index));
        }
    }
}