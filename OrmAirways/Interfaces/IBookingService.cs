using OrmAirways.Models;

namespace OrmAirways.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> ReserveTicketAsync(Guid flightId, Guid passengerId, Guid? seatId);
        Task CancelTicketAsync(Guid ticketId);
    }
}
