using System;
using System.Collections.Generic;

namespace RepositoryLayer.Entities;

public partial class BookingDetail
{
    public int BookingReservationId { get; set; }

    public int RoomId { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal? ActualPrice { get; set; }

    public virtual RoomInformation Room { get; set; }

    public BookingDetail(int bookingReservationId, int roomId, DateOnly startDate, DateOnly endDate, decimal? actualPrice)
    {
        BookingReservationId = bookingReservationId;
        RoomId = roomId;
        StartDate = startDate;
        EndDate = endDate;
        ActualPrice = actualPrice;
    }
}
