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
        private readonly IGenericRepository<BookingReservation> _bookingReservationRepo;
        public BookingReservationService(IGenericRepository<BookingReservation> bookingReservationRepo, IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _bookingReservationRepo = bookingReservationRepo;
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

        public async Task<IReadOnlyList<BookingReservationDTO>> GetBookingReservationsForManageAsync()
        {
            var spec = new BookingReservationSpecification(default(DateTime), default(DateTime));
            var bookingReservations = await _unitOfWork.Repository<BookingReservation>().ListAsync(spec);

            return _mapper.Map<IReadOnlyList<BookingReservation>, IReadOnlyList<BookingReservationDTO>>(bookingReservations);
        }

        public async Task<IReadOnlyList<BookingReservationReportStatisticDTO>> GetBookingReservationsForReportAsync(DateTime startDate, DateTime endDate)
        {

            var spec = new BookingReservationSpecification(startDate, endDate);
            var bookingReservations = await _unitOfWork.Repository<BookingReservation>().ListAsync(spec);
            return _mapper.Map<IReadOnlyList<BookingReservation>, IReadOnlyList<BookingReservationReportStatisticDTO>>(bookingReservations);
        }

        public async Task MakeReservation(int customerId, IReadOnlyList<BasketItemDTO> basketItems)
        {
            decimal totalPrice = 0;
            var booking = await _unitOfWork.Repository<BookingReservation>().ListAllAsync();
            var revId = 1;
            if(booking.Any())
            {
                revId = booking.Max(b => b.Id) + 1;
            }
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

            var reservation = new BookingReservation(revId, totalPrice, customerId, 0, bookingDetails);

            _unitOfWork.Repository<BookingReservation>().Add(reservation);
            await _unitOfWork.Complete();
        }

        public async Task ApproveBookingReservation(int id)
        {
            var bookingReservation = await _bookingReservationRepo.GetByIdAsync(id);
            bookingReservation.BookingStatus = 1; 
            _bookingReservationRepo.Update(bookingReservation);
            await _bookingReservationRepo.SaveAllAsync();
        }

    }
}
