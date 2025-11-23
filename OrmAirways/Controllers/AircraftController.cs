using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AircraftController(
        IAircraftRepository aircraftRepository,
        ISeatRepository seatRepository
    ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await aircraftRepository.GetAll());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (!id.HasValue) 
                return BadRequest();
            
            var aircraft = await aircraftRepository.GetById(id.Value);
            if (aircraft == null) 
                return NotFound();
            
            return View(aircraft);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aircraft aircraft)
        {
            if (!ModelState.IsValid) 
                return View(aircraft);

            try
            {
                if (aircraft.Id == Guid.Empty) aircraft.Id = Guid.NewGuid();

                await aircraftRepository.Create(aircraft);

                string[] seatPattern = { "Janela", "Corredor", "Corredor", "Janela" };

                for (int i = 0; i < aircraft.Capacity; i++)
                {
                    int patternIndex = i % seatPattern.Length;

                    var seat = new Seat
                    {
                        Id = Guid.NewGuid(),
                        AircraftId = aircraft.Id,
                        Code = $"{(i + 1)}",
                        ClassType = "Economy"
                    };

                    await seatRepository.Create(seat);
                }

                TempData["SuccessMessage"] = "Aeronave cadastrada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(aircraft);
            }
        }

        public async Task<IActionResult> Update(Guid? id)
        {
            if (!id.HasValue) 
                return BadRequest();
            
            var aircraft = await aircraftRepository.GetById(id.Value);
            if (aircraft == null)
                return NotFound();
            
            return View(aircraft);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, Aircraft aircraft)
        {
            if (id != aircraft.Id) 
                return BadRequest();

            if (!ModelState.IsValid) 
                return View(aircraft);

            try
            {
                await aircraftRepository.Update(aircraft);
                TempData["SuccessMessage"] = "Aeronave atualizada!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Erro ao atualizar.");
                return View(aircraft);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var aircraft = await aircraftRepository.GetById(id);
            if (aircraft == null) 
                return NotFound();

            await aircraftRepository.Delete(aircraft);
            TempData["SuccessMessage"] = "Aeronave removida com sucesso.";
            return RedirectToAction(nameof(Index));
        }
    }
}