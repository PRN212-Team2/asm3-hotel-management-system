using AutoMapper;
using BusinessServiceLayer.DTOs;
using PresentationLayer.Models;
using PresentationLayer.ViewModels;
using RepositoryLayer.Entities;
namespace PresentationLayer.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(d => d.CustomerBirthday, o => o.MapFrom(s => s.CustomerBirthday.ToDateTime(new TimeOnly())));
            CreateMap<CustomerToAddOrUpdateDTO, Customer>()
                .ForMember(d => d.CustomerBirthday, o => o.MapFrom(s => DateOnly.FromDateTime(s.CustomerBirthday)));
            CreateMap<CreateCustomerViewModel, CustomerToAddOrUpdateDTO>();
            CreateMap<UpdateCustomerViewModel, CustomerToAddOrUpdateDTO>();
            CreateMap<Customer, UserDTO>()
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.CustomerFullName))
                .ForMember(d => d.Birthday, o => o.MapFrom(s => s.CustomerBirthday.ToDateTime(new TimeOnly())));
            CreateMap<UserDTO, UserAccountModel>();
            CreateMap<BookingReservation, BookingReservationDTO>()
                .ForMember(d => d.BookingDate, o => o.MapFrom(s => s.BookingDate.ToDateTime(new TimeOnly())))
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.CustomerFullName));
            CreateMap<CustomerProfileViewModel, CustomerToAddOrUpdateDTO>();
            CreateMap<BookingReservation, BookingReservationReportStatisticDTO>()
                .ForMember(d => d.BookingDate, o => o.MapFrom(s => s.BookingDate.ToDateTime(new TimeOnly())))
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.CustomerFullName));
            CreateMap<RoomInformation, RoomInformationDTO>()
                .ForMember(d => d.RoomTypeName, o => o.MapFrom(s => s.RoomType.RoomTypeName))
                .ForMember(d => d.TypeDescription, o => o.MapFrom(s => s.RoomType.TypeDescription))
                .ForMember(d => d.TypeNote, o => o.MapFrom(s => s.RoomType.TypeNote));
            CreateMap<BookingDetail, BookingDetailDTO>()
                .ForMember(d => d.RoomNumber, o => o.MapFrom(s => s.Room.RoomNumber))
                .ForMember(d => d.StartDate, o => o.MapFrom(s => s.StartDate.ToDateTime(new TimeOnly())))
                .ForMember(d => d.EndDate, o => o.MapFrom(s => s.EndDate.ToDateTime(new TimeOnly())));
            CreateMap<BookingReservation, BookingReservationDetailDTO>()
                .ForMember(d => d.BookingDate, o => o.MapFrom(s => s.BookingDate.ToDateTime(new TimeOnly())))
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.CustomerFullName));
            CreateMap<RoomType, RoomTypeDTO>();
            CreateMap<BasketItem, BasketItemDTO>();
            CreateMap<RoomInformationToAddOrUpdateDTO, RoomInformation>();
            CreateMap<CreateRoomInformationViewModel, RoomInformationToAddOrUpdateDTO>();
            CreateMap<UpdateRoomInformationViewModel, RoomInformationToAddOrUpdateDTO>();

        }
    }
}
