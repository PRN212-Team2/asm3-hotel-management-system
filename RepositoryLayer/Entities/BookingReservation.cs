using System;
using System.Collections.Generic;

namespace RepositoryLayer.Entities;

public partial class BookingReservation : BaseEntity
{
    public DateOnly BookingDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public int CustomerId { get; set; }

    public byte? BookingStatus { get; set; }

    public virtual IReadOnlyList<BookingDetail> BookingDetails { get; set; }

    public virtual Customer Customer { get; set; }

    public BookingReservation() { }

    public BookingReservation(int id, decimal? totalPrice, int customerId, byte? bookingStatus, IReadOnlyList<BookingDetail> bookingDetails)
    {
        Id = id;
        BookingDate = DateOnly.FromDateTime(DateTime.Now);
        TotalPrice = totalPrice;
        CustomerId = customerId;
        BookingStatus = bookingStatus;
        BookingDetails = bookingDetails;
    }
}
