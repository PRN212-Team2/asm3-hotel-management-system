using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Specifications;

namespace BusinessServiceLayer.Services
{
    public class BookingReservationService : IBookingReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingReservationService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookingReservationDetailDTO> GetBookingReservationDetailByIdAsync(int id)
        {
            var spec = new BookingReservationSpecification(id);
            var bookingReservation = await _unitOfWork.Repository<BookingReservation>().GetEntityWithSpec(spec);
            return _mapper.Map<BookingReservation, BookingReservationDetailDTO>(bookingReservation);
        }

        public async Task<IReadOnlyList<BookingReservationDTO>> GetBookingReservationsByCustomerIdAsync(int customerId)
        {
            var spec = new BookingReservationForCustomerSpecification(customerId);
            var bookingReservations = await _unitOfWork.Repository<BookingReservation>().ListAsync(spec);
            return _mapper.Map<IReadOnlyList<BookingReservation>, IReadOnlyList<BookingReservationDTO>>(bookingReservations);
        }

        public async Task<IReadOnlyList<BookingReservationReportStatisticDTO>> GetBookingReservationsAsync(DateTime startDate, DateTime endDate)
        {

            var spec = new BookingReservationSpecification(startDate, endDate);
            var bookingReservations = await _unitOfWork.Repository<BookingReservation>().ListAsync(spec);
            return _mapper.Map<IReadOnlyList<BookingReservation>, IReadOnlyList<BookingReservationReportStatisticDTO>>(bookingReservations);
        }

        public async Task MakeReservation(int customerId, IReadOnlyList<BasketItemDTO> basketItems)
        {
            decimal totalPrice = 0;
            var booking = await _unitOfWork.Repository<BookingReservation>().ListAllAsync();
            var revId = booking.Max(b => b.Id) + 1;
            List<BookingDetail> bookingDetails = new List<BookingDetail>();
            
            foreach(var item in basketItems)
            {
                var room = await _unitOfWork.Repository<RoomInformation>().GetByIdAsync(item.RoomId);
                int numberOfDays = (item.EndDate - item.StartDate).Days + 1;
                decimal roomPrice = room.RoomPricePerDay.Value * numberOfDays;
                totalPrice += roomPrice;
                var bookingDetail = new BookingDetail(revId, item.RoomId, 
                    DateOnly.FromDateTime(item.StartDate), 
                    DateOnly.FromDateTime(item.EndDate), 
                    room.RoomPricePerDay);
                bookingDetails.Add(bookingDetail);
            }

            var reservation = new BookingReservation(revId, totalPrice, customerId, 1, bookingDetails);

            _unitOfWork.Repository<BookingReservation>().Add(reservation);
            await _unitOfWork.Complete();
        }
    }
}
