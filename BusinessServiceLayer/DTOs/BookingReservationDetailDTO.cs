using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServiceLayer.DTOs
{
    public class BookingReservationDetailDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public bool BookingStatus { get; set; }
        public IReadOnlyList<BookingDetailDTO> BookingDetails { get; set; }
    }
}
