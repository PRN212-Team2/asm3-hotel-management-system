using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Data
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly FuminiHotelManagementContext _context;

        public BookingDetailRepository(FuminiHotelManagementContext context)
        {
            _context = context;
        }

        public async Task<BookingDetail> GetBookingDetailByRoomId(int roomId)
        {
            return await _context.Set<BookingDetail>().FirstOrDefaultAsync(b => b.RoomId == roomId);
        }
    }
}
