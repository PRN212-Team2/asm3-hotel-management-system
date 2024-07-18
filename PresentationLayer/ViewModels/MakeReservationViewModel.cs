﻿using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using Microsoft.IdentityModel.Tokens;
using PresentationLayer.Commands;
using PresentationLayer.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase
    {
        private readonly IRoomService _roomService;
        private readonly IBookingReservationService _bookingReservationService;
        private readonly RoomInformationDetailsViewModel _roomInformationDetailsViewModel;
        private readonly IMapper _mapper;

        private ObservableCollection<MakeReservationRoomItemViewModel> _rooms;

        // List of room information
        public ObservableCollection<MakeReservationRoomItemViewModel> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        // Basket Items to temporarily store bookings
        public ObservableCollection<BasketItem> BasketItems { get; set; }

        // Retrieve list of room types for dropdown value
        public ObservableCollection<RoomTypeDTO> RoomTypes { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalPrice { get; set; }

        public RelayCommand MakeReservationCommand { get; set; }

        public MakeReservationViewModel(IRoomService roomService, 
           IBookingReservationService bookingReservationService , 
           RoomInformationDetailsViewModel roomInformationDetailsViewModel, 
           IMapper mapper)
        {
            _roomService = roomService;
            _bookingReservationService = bookingReservationService;
            _roomInformationDetailsViewModel = roomInformationDetailsViewModel;
            _mapper = mapper;
            BasketItems = BasketManager.GetBasketItems();
            MakeReservationCommand = new RelayCommand(async o => await MakeReservation(o), o => true);
        }

        public async Task MakeReservation(object obj)
        {
            if(BasketManager.BasketItems.IsNullOrEmpty())
            {
                MessageBox.Show("No Room Selected for Reservation!");
            }
            else
            {
                IReadOnlyList<BasketItemDTO> basketItems = _mapper
                .Map<IReadOnlyList<BasketItem>, IReadOnlyList<BasketItemDTO>>(BasketItems.ToList());
                await _bookingReservationService.MakeReservation(CustomerId, basketItems);
                BasketManager.BasketItems.Clear();
                MessageBox.Show("Reservation Created Successfully!");
            }
        }

        public async Task GetRoomTypesAsync()
        {
            var roomTypes = await _roomService.GetRoomTypesAsync();
            RoomTypes = new ObservableCollection<RoomTypeDTO>(roomTypes);
        }

        public async Task GetRoomsAsync()
        {
            var rooms = await _roomService.GetRoomsWithTypeAsync();

            var roomObservable = new ObservableCollection<MakeReservationRoomItemViewModel>();
            foreach (var room in rooms)
            {
                var roomItem = new MakeReservationRoomItemViewModel(_roomInformationDetailsViewModel);
                roomItem.Id = room.Id;
                roomItem.RoomNumber = room.RoomNumber;
                roomItem.RoomMaxCapacity = room.RoomMaxCapacity;
                roomItem.RoomPricePerDay = room.RoomPricePerDay;
                roomItem.RoomStatus = room.RoomStatus;
                roomItem.RoomTypeId = room.RoomTypeId;
                roomItem.RoomTypeName = room.RoomTypeName;
                roomObservable.Add(roomItem);
            }

            Rooms = roomObservable;
        }

    }
}
