using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using PresentationLayer.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class ListBookingReservationViewModel : ViewModelBase
    {
        private readonly IBookingReservationService _bookingReservationService;
        private ObservableCollection<BookingReservationReportStatisticDTO> _bookingReservations;
        public ObservableCollection<BookingReservationReportStatisticDTO> BookingReservationList
        {
            get => _bookingReservations;
            set
            {
                _bookingReservations = value;
                OnPropertyChanged(nameof(BookingReservationList));
            }
        }

        public RelayCommand UpdateReservationStatusCommand { get; set; }

        public ListBookingReservationViewModel(IBookingReservationService bookingReservationService)
        {
            _bookingReservationService = bookingReservationService;
            UpdateReservationStatusCommand = new RelayCommand(async o => await UpdateReservationStatusAsync(o), o => true);
        }


        public async Task GetBookingReservationsForManageAsync()
        {
            var bookingReservation = await _bookingReservationService.GetPendingBookingReservationsAsync();
            BookingReservationList = new ObservableCollection<BookingReservationReportStatisticDTO>(bookingReservation);
        }

        private async Task UpdateReservationStatusAsync(object obj)
        {
            int id = (int)obj;
            var booking = BookingReservationList.FirstOrDefault(b => b.Id == id);

            if (booking != null && !booking.BookingStatus)
            {
                MessageBoxResult ans = MessageBox.Show("Do you want to approve this reservation?", "Approval",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (ans == MessageBoxResult.Yes)
                {
                    await _bookingReservationService.ApproveBookingReservation(id);
                    var bookingReservation = await _bookingReservationService.GetPendingBookingReservationsAsync();
                    BookingReservationList = new ObservableCollection<BookingReservationReportStatisticDTO>(bookingReservation);
                }
            }
        }
    }
}
