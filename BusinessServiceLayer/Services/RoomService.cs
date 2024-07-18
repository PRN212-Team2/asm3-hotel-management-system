using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly IGenericRepository<RoomInformation> _roomInformationRepository;
        private readonly IGenericRepository<RoomType> _roomTypeRepository;
        private readonly IBookingDetailRepository _bookingDetailRepository;
        private readonly IMapper _mapper;

        public RoomService(IGenericRepository<RoomInformation> roomInformationRepository, 
            IGenericRepository<RoomType> roomTypeRepository, IBookingDetailRepository bookingDetailRepository,
            IMapper mapper)
        {
            _roomInformationRepository = roomInformationRepository;
            _roomTypeRepository = roomTypeRepository;
            _bookingDetailRepository = bookingDetailRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<RoomInformationDTO>> GetRoomsWithTypeAsync()
        {
            var spec = new RoomInformationSpecification(true);
            var rooms = await _roomInformationRepository.ListAsync(spec);
            return _mapper.Map<IReadOnlyList<RoomInformation>, IReadOnlyList<RoomInformationDTO>>(rooms);
        }

        public async Task<RoomInformationDTO> GetRoomByIdWithTypeAsync(int id)
        {
            var spec = new RoomInformationSpecification(id);
            var room = await _roomInformationRepository.GetEntityWithSpec(spec);
            if (room == null) return null;
            return _mapper.Map<RoomInformation, RoomInformationDTO>(room);
        }

        public async Task CreateRoomAsync(RoomInformationToAddOrUpdateDTO room)
        {
            if (room == null) throw new ArgumentNullException(nameof(RoomInformationToAddOrUpdateDTO));
            RoomInformation roomToAdd = _mapper.Map<RoomInformationToAddOrUpdateDTO, RoomInformation>(room);
            _roomInformationRepository.Add(roomToAdd);
            await _roomInformationRepository.SaveAllAsync();
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room = await _roomInformationRepository.GetByIdAsync(id);
            if (room == null) return;
            var booking = await _bookingDetailRepository.GetBookingDetailByRoomId(id);

            if (booking != null)
            {
                // If the room is booked and saved in to booking history then set status to false (soft delete)
                room.RoomStatus = 0;
                _roomInformationRepository.Update(room);
                await _roomInformationRepository.SaveAllAsync();
            }
            else
            {
                // Else hard delete the room from database
                _roomInformationRepository.Delete(room);
                await _roomInformationRepository.SaveAllAsync();
            }   
        }
        public async Task UpdateRoomAsync(RoomInformationToAddOrUpdateDTO updatedRoom, int id)
        {
            var spec = new RoomInformationSpecification(id);
            RoomInformation existingRoom = await _roomInformationRepository.GetEntityWithSpec(spec);
            if (existingRoom == null) return;
            _mapper.Map(updatedRoom, existingRoom);
            await _roomInformationRepository.SaveAllAsync();
        }
        public async Task<IReadOnlyList<RoomTypeDTO>> GetRoomTypesAsync()
        {
            var roomTypes = await _roomTypeRepository.ListAllAsync();
            return _mapper.Map<IReadOnlyList<RoomType>, IReadOnlyList<RoomTypeDTO>>(roomTypes);
        }

        public async Task<IReadOnlyList<RoomInformationDTO>> GetRoomInformationForManageAsync()
        {
            var spec = new RoomInformationSpecification();
            var rooms = await _roomInformationRepository.ListAsync(spec);
            return _mapper.Map<IReadOnlyList<RoomInformation>, IReadOnlyList<RoomInformationDTO>>(rooms);
        }

        public async Task<bool> CheckRoomNumberExisted(string roomNumber)
        {
            var spec = new RoomInformationSpecification(roomNumber);
            var room = await _roomInformationRepository.GetEntityWithSpec(spec);
            if (room != null) return true;
            return false;
        }
    }
}
