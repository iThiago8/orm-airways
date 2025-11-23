using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FlightController(
        IFlightRepository flightRepository,
        IAirportRepository airportRepository,
        IAircraftRepository aircraftRepository
    ) : Controller
    {

        private async Task PopulateViewBags(Guid? selectedOrigin = null, Guid? selectedDestination = null, Guid? selectedAircraft = null)
        {
            var airports = await airportRepository.GetAll();
            var aircrafts = await aircraftRepository.GetAll();

            ViewBag.OriginId = new SelectList(airports, "Id", "IataCode", selectedOrigin);
            ViewBag.DestinationId = new SelectList(airports, "Id", "IataCode", selectedDestination);

            var aircraftList = aircrafts.Select(a => new {
                a.Id,
                DisplayText = $"{a.Model} ({a.RegistrationNumber})"
            });
            ViewBag.AircraftId = new SelectList(aircraftList, "Id", "DisplayText", selectedAircraft);
        }

        public async Task<IActionResult> Index()
        {
            return View(await flightRepository.GetAll());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (!id.HasValue) 
                return BadRequest();
            
            var flight = await flightRepository.GetById(id.Value);

            if (flight == null) 
                
                return NotFound();
            return View(flight);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight flight)
        {
            if (flight.OriginId == flight.DestinationId)
                ModelState.AddModelError("DestinationId", "O aeroporto de destino não pode ser o mesmo de origem.");

            if (flight.ArrivalTime <= flight.DepartureTime)
                ModelState.AddModelError("ArrivalTime", "A data/hora de chegada deve ser posterior à partida.");

            if (!ModelState.IsValid)
            {
                await PopulateViewBags(flight.OriginId, flight.DestinationId, flight.AircraftId);
                return View(flight);
            }

            try
            {
                flight.Id = Guid.NewGuid();
                await flightRepository.Create(flight);
                TempData["SuccessMessage"] = "Voo programado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Erro ao agendar voo: " + ex.Message);
                await PopulateViewBags(flight.OriginId, flight.DestinationId, flight.AircraftId);
                return View(flight);
            }
        }

        public async Task<IActionResult> Update(Guid? id)
        {
            if (!id.HasValue) 
                return BadRequest();

            var flight = await flightRepository.GetById(id.Value);
            
            if (flight == null) 
                return NotFound();

            await PopulateViewBags(flight.OriginId, flight.DestinationId, flight.AircraftId);
            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, Flight flight)
        {
            if (id != flight.Id) 
                return BadRequest();

            if (flight.OriginId == flight.DestinationId)
                ModelState.AddModelError("DestinationId", "O aeroporto de destino não pode ser o mesmo de origem.");

            if (flight.ArrivalTime <= flight.DepartureTime)
                ModelState.AddModelError("ArrivalTime", "A data/hora de chegada deve ser posterior à partida.");

            if (!ModelState.IsValid)
            {
                await PopulateViewBags(flight.OriginId, flight.DestinationId, flight.AircraftId);
                return View(flight);
            }

            try
            {
                await flightRepository.Update(flight);
                TempData["SuccessMessage"] = "Voo atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Erro ao atualizar voo.");
                await PopulateViewBags(flight.OriginId, flight.DestinationId, flight.AircraftId);
                return View(flight);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var flight = await flightRepository.GetById(id);
            if (flight == null) return NotFound();

            if (flight.Bookings != null && flight.Bookings.Count != 0)
            {
                TempData["ErrorMessage"] = "Não é possível excluir este voo pois já existem passagens vendidas para ele.";
            }
            else
            {
                await flightRepository.Delete(flight);
                TempData["SuccessMessage"] = "Voo cancelado e removido do sistema.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}