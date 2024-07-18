using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Specifications
{
    public class BookingReservationForCustomerSpecification : BaseSpecification<BookingReservation>
    {
        public BookingReservationForCustomerSpecification(int customerId) : base(x => x.CustomerId == customerId)
        {
            AddCustomInclude(x => x.Include(x => x.BookingDetails).ThenInclude(x => x.Room));
            AddOrderByDescending(x => x.BookingDate);
        }
    }
}
