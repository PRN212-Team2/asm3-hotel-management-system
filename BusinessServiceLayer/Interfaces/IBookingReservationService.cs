using BusinessServiceLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Interfaces
{
    public interface IBookingReservationService
    {
        Task<IReadOnlyList<BookingReservationDTO>> GetBookingReservationsByCustomerIdAsync(int customerId);
        Task<IReadOnlyList<BookingReservationReportStatisticDTO>> GetBookingReservationsForReportAsync(DateTime startDate, DateTime endDate);
        Task<BookingReservationDetailDTO> GetBookingReservationDetailByIdAsync(int id);
        Task MakeReservation(int customerId, IReadOnlyList<BasketItemDTO> basketItems);
        Task<IReadOnlyList<BookingReservationReportStatisticDTO>> GetPendingBookingReservationsAsync();
        Task ApproveBookingReservation(int id);
    }
}
