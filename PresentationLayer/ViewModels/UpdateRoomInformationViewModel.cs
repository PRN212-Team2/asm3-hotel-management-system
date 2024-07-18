using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using BusinessServiceLayer.Services;
using PresentationLayer.Commands;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class UpdateRoomInformationViewModel : ViewModelBase
    {
        private readonly IRoomService _roomInformationService;
        private readonly IMapper _mapper;
        public event EventHandler RoomInformationUpdated;
        public RelayCommand UpdateRoomInformationCommand { get; set; }

        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public bool RoomStatus { get; set; }
        public decimal RoomPricePerDay { get; set; }

        public ObservableCollection<RoomTypeDTO> RoomTypes { get; set; }

        private int? _roomTypeId;
        public int? RoomTypeId
        {
            get => _roomTypeId;
            set
            {
                _roomTypeId = value;
                OnPropertyChanged(nameof(RoomTypeId));
            }
        }

        public UpdateRoomInformationViewModel(IRoomService roomInformationService, IMapper mapper)
        {
            _roomInformationService = roomInformationService;
            _mapper = mapper;
            UpdateRoomInformationCommand = new RelayCommand(async o => await UpdateRoomInformationAsync(o), CanExecuteUpdateRoomInformationCommand);
        }

        public async Task LoadRoomInformationDetail(int id)
        {
            var room = await _roomInformationService.GetRoomByIdWithTypeAsync(id);
            if (room != null)
            {
                Id = room.Id;
                RoomNumber = room.RoomNumber;
                RoomDetailDescription = room.RoomDetailDescription;
                RoomMaxCapacity = room.RoomMaxCapacity;
                RoomTypeId = room.RoomTypeId;
                RoomStatus = room.RoomStatus;
                RoomPricePerDay = room.RoomPricePerDay;
            }
        }

        private bool CanExecuteUpdateRoomInformationCommand(object obj)
        {
            if (
                string.IsNullOrWhiteSpace(RoomNumber) ||
                string.IsNullOrWhiteSpace(RoomDetailDescription) ||
                RoomMaxCapacity <= 0 ||
                RoomTypeId <= 0 ||
                RoomPricePerDay <= 0)
            {
                return false;
            }
            return true;
        }

        public async Task GetRoomTypesAsync()
        {
            var roomTypes = await _roomInformationService.GetRoomTypesAsync();
            RoomTypes = new ObservableCollection<RoomTypeDTO>(roomTypes);
        }

        private async Task UpdateRoomInformationAsync(object obj)
        {
            var oldRoom = await _roomInformationService.GetRoomByIdWithTypeAsync(Id);
            bool roomNumberExisted = await _roomInformationService.CheckRoomNumberExisted(RoomNumber);

            if (oldRoom.RoomNumber != RoomNumber && roomNumberExisted)
            {
                MessageBox.Show("Room Number existed!");
            }
            else
            {
                var roomToUpdate = _mapper.Map<UpdateRoomInformationViewModel, RoomInformationToAddOrUpdateDTO>(this);
                await _roomInformationService.UpdateRoomAsync(roomToUpdate, Id);
                MessageBox.Show("Room Information Updated Successfully");
                RoomInformationUpdated?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
