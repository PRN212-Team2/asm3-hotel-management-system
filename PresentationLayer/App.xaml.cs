using BusinessServiceLayer.Interfaces;
using BusinessServiceLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PresentationLayer.Services;
using PresentationLayer.ViewModels;
using PresentationLayer.Views;
using RepositoryLayer.Data;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System.Configuration;
using System.Data;
using System.Windows;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Config { get; private set; }
        public static IHost AppHost { get; private set; }

        public App()
        {
            // Reads appsettings.json file
            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Sets up AppHost
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<LoginView>();
                    services.AddDbContext<FuminiHotelManagementContext>(opt =>
                    {
                        opt.UseSqlServer(Config.GetConnectionString("DefaultConnection"));
                    });
                    services.AddSingleton<INavigationService, NavigationService>();
                    services.AddSingleton<Func<Type, ViewModelBase>>(provider => viewModelType =>
                        (ViewModelBase)provider.GetRequiredService(viewModelType));
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<ListCustomersViewModel>();
                    services.AddSingleton<ListReportStatisticsViewModel>();
                    services.AddSingleton<ManageCustomerView>();
                    services.AddSingleton<CreateCustomerViewModel>();
                    services.AddSingleton<UpdateCustomerViewModel>();
                    services.AddSingleton<DeleteCustomerViewModel>();
                    services.AddSingleton<ListBookingReservationHistoryViewModel>();
                    services.AddSingleton<CustomerProfileViewModel>();
                    services.AddSingleton<LoginViewModel>();
                    services.AddSingleton<BookingReservationDetailViewModel>();
                    services.AddSingleton<MakeReservationViewModel>();
                    services.AddSingleton<RoomInformationDetailsViewModel>();
                    services.AddSingleton<ListRoomInformationViewModel>();
                    services.AddSingleton<CreateRoomInformationViewModel>();
                    services.AddSingleton<DeleteRoomInformationViewModel>();
                    services.AddSingleton<UpdateRoomInformationViewModel>();
                    services.AddSingleton<ListBookingReservationViewModel>();
                    services.AddSingleton<Func<Type, ViewModelBase>>(services => viewModelType
                    => (ViewModelBase)services.GetRequiredService(viewModelType));
                    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                    services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();
                    services.AddScoped<IUnitOfWork, UnitOfWork>();
                    services.AddScoped<ICustomerService, CustomerService>();
                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<IBookingReservationService, BookingReservationService>();
                    services.AddScoped<IRoomService, RoomService>();
                    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs eventArgs)
        {
            await AppHost!.StartAsync();
            var context = AppHost.Services.GetRequiredService<FuminiHotelManagementContext>();
            var logger = AppHost.Services.GetRequiredService<ILogger<App>>();
            try
            {
                // Migrate Database
                await context.Database.MigrateAsync();

                // Seed Data
                await FuminiHotelManagementContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "A message occured during migration");
            }

            var loginView = AppHost.Services.GetRequiredService<LoginView>();
            var loginViewModel = AppHost.Services.GetRequiredService<LoginViewModel>();
            var mainViewModel = AppHost.Services.GetService<MainViewModel>();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (loginViewModel.IsViewVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainWindow(mainViewModel, loginViewModel);
                    mainView.Show();
                }
            };

            base.OnStartup(eventArgs);
        }

        protected override async void OnExit(ExitEventArgs eventArgs)
        {
            await AppHost!.StopAsync();
            base.OnExit(eventArgs);
        }
    }

}
