using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Specifications
{
    public class BookingReservationSpecification : BaseSpecification<BookingReservation>
    {
        public BookingReservationSpecification(DateTime startDate, DateTime endDate) 
            : base(x => 
            startDate == default(DateTime) || endDate == default(DateTime)
            || (x.BookingDate >= DateOnly.FromDateTime(startDate) 
            && x.BookingDate <= DateOnly.FromDateTime(endDate))
            )
        {
            AddOrderByDescending(x => x.BookingDate);
        }

        public BookingReservationSpecification(int id) : base(x => x.Id == id)
        {
            AddCustomInclude(x => x.Include(x => x.BookingDetails).ThenInclude(x => x.Room));
        }
    }
}
