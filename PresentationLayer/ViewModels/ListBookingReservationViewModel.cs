using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using PresentationLayer.Services;
using PresentationLayer.Views;
using RepositoryLayer.Entities;
using System.Collections.ObjectModel;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class ListBookingReservationViewModel : ViewModelBase
    {
        private readonly IBookingReservationService _bookingReservationService;
        private ObservableCollection<BookingReservationItemViewModel> _bookingReservations;
        private readonly BookingReservationDetailViewModel _bookingReservationDetailViewModel;
        public ObservableCollection<BookingReservationItemViewModel> BookingReservationList
        {
            get => _bookingReservations;
            set
            {
                _bookingReservations = value;
                OnPropertyChanged(nameof(BookingReservationList));
            }
        }

        public RelayCommand UpdateReservationStatusCommand { get; set; }
        public ListBookingReservationViewModel(IBookingReservationService bookingReservationService, 
            BookingReservationDetailViewModel bookingReservationDetailViewModel)
        {
            _bookingReservationService = bookingReservationService;
            _bookingReservationDetailViewModel = bookingReservationDetailViewModel;
            UpdateReservationStatusCommand = new RelayCommand(async o => await UpdateReservationStatusAsync(o), o => true);
        }


        public async Task GetBookingReservationsForManageAsync()
        {
            var bookingReservations = await _bookingReservationService.GetBookingReservationsForManageAsync();
            var bookingObservable = new ObservableCollection<BookingReservationItemViewModel>();
            foreach (var booking in bookingReservations)
            {
                var bookingDetail = new BookingReservationItemViewModel(_bookingReservationDetailViewModel);
                bookingDetail.Id = booking.Id;
                bookingDetail.BookingDate = booking.BookingDate;
                bookingDetail.BookingStatus = booking.BookingStatus;
                bookingDetail.TotalPrice = booking.TotalPrice;
                bookingDetail.CustomerName = booking.CustomerName;
                bookingObservable.Add(bookingDetail);
            }

            BookingReservationList = bookingObservable;
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
                    await GetBookingReservationsForManageAsync();
                }
            }
        }
    }
}
