﻿using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class DeleteRoomInformationViewModel
    {
        private readonly IRoomService _roomInformationService;

        public event EventHandler RoomInformationDeleted;

        public int RoomId { get; set; }

        public RelayCommand DeleteRoomInformationCommand { get; set; }

        public DeleteRoomInformationViewModel(IRoomService roomInformationService)
        {
            _roomInformationService = roomInformationService;
            DeleteRoomInformationCommand = new RelayCommand(async o => await DeleteRoomInformation(o), o => true);
        }

        private async Task DeleteRoomInformation(object obj)
        {
            await _roomInformationService.DeleteRoomAsync(RoomId);
            RoomInformationDeleted?.Invoke(this, EventArgs.Empty);
            MessageBox.Show("Delete Room Successfully");
        }
    }
}
