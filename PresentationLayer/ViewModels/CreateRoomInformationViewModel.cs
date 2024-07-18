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
    public class CreateRoomInformationViewModel : ViewModelBase
    {
        private readonly IRoomService _roomInformationService;
        private readonly IMapper _mapper;
        public event EventHandler RoomInformationCreated;
        public RelayCommand CreateRoomInformationCommand { get; set; }
        public string RoomNumber { get; set; }
        public string RoomDetailDescription { get; set; }
        public int RoomMaxCapacity { get; set; }
        public bool RoomStatus { get; set; } = true;
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

        public CreateRoomInformationViewModel(IRoomService roomInformationService, IMapper mapper)
        {
            _roomInformationService = roomInformationService;
            _mapper = mapper;
            CreateRoomInformationCommand = new RelayCommand(async o => await CreateRoomInformationAsync(o), CanExecuteCreateRoomInformationCommand);
        }

        private bool CanExecuteCreateRoomInformationCommand(object obj)
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

        private async Task CreateRoomInformationAsync(object obj)
        {
            bool roomNumberExisted = await _roomInformationService.CheckRoomNumberExisted(RoomNumber);
            if (roomNumberExisted)
            {
                MessageBox.Show("Room Number existed!");
            }
            else
            {
                var roomToCreate = _mapper.Map<CreateRoomInformationViewModel, RoomInformationToAddOrUpdateDTO>(this);
                await _roomInformationService.CreateRoomAsync(roomToCreate);
                MessageBox.Show("Room Created Successfully!");
                RoomInformationCreated?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
