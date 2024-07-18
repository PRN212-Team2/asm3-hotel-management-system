using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IBookingDetailRepository
    {
        Task<BookingDetail> GetBookingDetailByRoomId(int roomId);
    }
}
