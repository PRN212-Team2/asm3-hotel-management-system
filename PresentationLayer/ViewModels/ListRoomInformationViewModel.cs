using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class ListRoomInformationViewModel : ViewModelBase
    {
        private readonly IRoomService _roomInformationService;
        private readonly CreateRoomInformationViewModel _createRoomInformationViewModel;
        private readonly UpdateRoomInformationViewModel _updateRoomInformationViewModel;
        private readonly DeleteRoomInformationViewModel _deleteRoomInformationViewModel;
        private ObservableCollection<RoomInformationItemViewModel> _rooms;
        public ObservableCollection<RoomInformationItemViewModel> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        // Retrieve list of room types for dropdown value
        public ObservableCollection<RoomTypeDTO> RoomTypes { get; set; }
        public RelayCommand ShowCreateRoomInformationWindow { get; set; }

        public ListRoomInformationViewModel(IRoomService roomInformationService,
            CreateRoomInformationViewModel createRoomInformationViewModel,
            UpdateRoomInformationViewModel updateRoomInformationViewModel,
            DeleteRoomInformationViewModel deleteRoomInformationViewModel)
        {
            _roomInformationService = roomInformationService;
            _createRoomInformationViewModel = createRoomInformationViewModel;
            _updateRoomInformationViewModel = updateRoomInformationViewModel;
            _deleteRoomInformationViewModel = deleteRoomInformationViewModel;
            _createRoomInformationViewModel.RoomInformationCreated += OnRoomInformationCreated;
            _deleteRoomInformationViewModel.RoomInformationDeleted += OnRoomInformationDeleted;
            _updateRoomInformationViewModel.RoomInformationUpdated += OnRoomInformationUpdated;
            ShowCreateRoomInformationWindow = new RelayCommand(async o => await ShowCreateWindow(o), o => true);
        }

        private async Task ShowCreateWindow(object obj)
        {
            await _createRoomInformationViewModel.GetRoomTypesAsync();
            CreateRoomInformationPopupView createRoomInformationWin = new CreateRoomInformationPopupView(_createRoomInformationViewModel);
            createRoomInformationWin.Show();
        }

        private async void OnRoomInformationCreated(object sender, EventArgs e)
        {
            await GetRoomInformationAsync();
        }

        private async void OnRoomInformationDeleted(object sender, EventArgs e)
        {
            await GetRoomInformationAsync();
        }

        private async void OnRoomInformationUpdated(object sender, EventArgs e)
        {
            await GetRoomInformationAsync();
        }

        public async Task GetRoomTypesAsync()
        {
            var roomTypes = await _roomInformationService.GetRoomTypesAsync();
            RoomTypes = new ObservableCollection<RoomTypeDTO>(roomTypes);
        }

        public async Task GetRoomInformationAsync()
        {
            var rooms = await _roomInformationService.GetRoomInformationForManageAsync();

            var roomInformationObservable = new ObservableCollection<RoomInformationItemViewModel>();
            foreach (var room in rooms)
            {
                var roomInformationDetail = new RoomInformationItemViewModel(_updateRoomInformationViewModel, 
                    _deleteRoomInformationViewModel, _roomInformationService);
                roomInformationDetail.Id = room.Id;
                roomInformationDetail.RoomNumber = room.RoomNumber;
                roomInformationDetail.RoomDetailDescription = room.RoomDetailDescription;
                roomInformationDetail.RoomMaxCapacity = room.RoomMaxCapacity;
                roomInformationDetail.RoomTypeId = room.RoomTypeId;
                roomInformationDetail.RoomTypeName = room.RoomTypeName;
                roomInformationDetail.TypeNote = room.TypeNote;
                roomInformationDetail.TypeDescription = room.TypeDescription;
                roomInformationDetail.RoomStatus = room.RoomStatus;
                roomInformationDetail.RoomPricePerDay = room.RoomPricePerDay;
                roomInformationObservable.Add(roomInformationDetail);
            }

            Rooms = roomInformationObservable;
        }
    }
}
