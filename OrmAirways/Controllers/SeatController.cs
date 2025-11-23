using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Controllers
{
    public class SeatController(
        ISeatRepository seatRepository,
        IAircraftRepository aircraftRepository
    ) : Controller
    {

        public async Task<IActionResult> Index(Guid? aircraftId)
        {
            var allSeats = await seatRepository.GetAll();
            var aircrafts = await aircraftRepository.GetAll();

            ViewBag.Aircrafts = new SelectList(aircrafts, "Id", "RegistrationNumber", aircraftId);
            ViewBag.SelectedAircraftId = aircraftId;

            if (aircraftId.HasValue)
            {
                var filteredSeats = allSeats?.Where(s => s.AircraftId == aircraftId.Value).OrderBy(s => s.Code).ToList();
                return View(filteredSeats);
            }

            return View(new List<Seat>());
        }

        public async Task<IActionResult> Update(Guid? id)
        {
            if (!id.HasValue) return BadRequest();

            var seat = await seatRepository.GetById(id.Value);
            if (seat == null) return NotFound();

            return View(seat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, Seat seat)
        {
            if (id != seat.Id) return BadRequest();


            try
            {
                var originalSeat = await seatRepository.GetById(id);
                if (originalSeat == null) return NotFound();

                originalSeat.Code = seat.Code;
                originalSeat.ClassType = seat.ClassType;

                await seatRepository.Update(originalSeat);

                TempData["SuccessMessage"] = "Assento atualizado!";

                return RedirectToAction(nameof(Index), new { aircraftId = originalSeat.AircraftId });
            }
            catch
            {
                ModelState.AddModelError("", "Erro ao atualizar.");
                return View(seat);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var seat = await seatRepository.GetById(id);
            if (seat == null) return NotFound();

            var aircraftId = seat.AircraftId;

            await seatRepository.Delete(seat);
            TempData["SuccessMessage"] = "Assento removido manualmente.";

            return RedirectToAction(nameof(Index), new { aircraftId });
        }
    }
}