using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using RepositoryLayer.Entities;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Data
{
    public class FuminiHotelManagementContextSeed
    {
        public static async Task SeedAsync(FuminiHotelManagementContext context)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            options.Converters.Add(new JsonStringEnumConverter());
            //Seed Customer Data
            if (!context.Customers.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                var data = await File.ReadAllTextAsync(path + @"/Data/SeedData/customers.json");

                var list = JsonSerializer.Deserialize<List<Customer>>(data, options);

                foreach (var item in list)
                {
                    context.Customers.Add(item);
                }

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Customer ON");
                await context.SaveChangesAsync();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Customer OFF");
                transaction.Commit();
            }

            //Seed Room Type Data
            if (!context.RoomTypes.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                var data = await File.ReadAllTextAsync(path + @"/Data/SeedData/roomTypes.json");

                var list = JsonSerializer.Deserialize<List<RoomType>>(data, options);

                foreach (var item in list)
                {
                    context.RoomTypes.Add(item);
                }

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT RoomType ON");
                await context.SaveChangesAsync();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT RoomType OFF");
                transaction.Commit();
            }

            //Seed Room Information Data
            if (!context.RoomInformations.Any())
            {
                using var transaction = context.Database.BeginTransaction();
                var data = await File.ReadAllTextAsync(path + @"/Data/SeedData/roomInformations.json");

                var list = JsonSerializer.Deserialize<List<RoomInformation>>(data, options);

                foreach (var item in list)
                {
                    context.RoomInformations.Add(item);
                }

                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT RoomInformation ON");
                await context.SaveChangesAsync();
                context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT RoomInformation OFF");
                transaction.Commit();
            }

            //Seed Booking Reservation Data
            if (!context.BookingReservations.Any())
            {
                var data = await File.ReadAllTextAsync(path + @"/Data/SeedData/bookingReservations.json");

                var list = JsonSerializer.Deserialize<List<BookingReservation>>(data, options);

                foreach (var item in list)
                {
                    context.BookingReservations.Add(item);
                }
                await context.SaveChangesAsync();
            }

            //Seed Booking Detail Data
            if (!context.BookingDetails.Any())
            {
                var data = await File.ReadAllTextAsync(path + @"/Data/SeedData/bookingDetails.json");

                var list = JsonSerializer.Deserialize<List<BookingDetail>>(data, options);

                foreach (var item in list)
                {
                    context.BookingDetails.Add(item);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
