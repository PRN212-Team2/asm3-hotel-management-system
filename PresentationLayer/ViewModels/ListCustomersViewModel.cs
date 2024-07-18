using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.ViewModels
{
    public class ListCustomersViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private readonly CreateCustomerViewModel _createCustomerViewModel;
        private readonly UpdateCustomerViewModel _updateCustomerViewModel;
        private readonly DeleteCustomerViewModel _deleteCustomerViewModel;
        private ObservableCollection<CustomerItemsViewModel> _customers;
        public ObservableCollection<CustomerItemsViewModel> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        public RelayCommand ShowCreateCustomerWindow {  get; set; }

        public ListCustomersViewModel(ICustomerService customerService, 
            CreateCustomerViewModel createCustomerViewModel,
            UpdateCustomerViewModel updateCustomerViewModel,
            DeleteCustomerViewModel deleteCustomerViewModel)
        {
            _customerService = customerService;
            _createCustomerViewModel = createCustomerViewModel;
            _updateCustomerViewModel = updateCustomerViewModel;
            _deleteCustomerViewModel = deleteCustomerViewModel;
            _createCustomerViewModel.CustomerCreated += OnCustomerCreated;
            _deleteCustomerViewModel.CustomerDeleted += OnCustomerDeleted;
            _updateCustomerViewModel.CustomerUpdated += OnCustomerUpdated;
            ShowCreateCustomerWindow = new RelayCommand(ShowCreateWindow, o => true);
        }

        private void ShowCreateWindow(object obj)
        {
            CreateCustomerPopupView createCustomerWin = new CreateCustomerPopupView(_createCustomerViewModel);
            createCustomerWin.Show();
        }

        private async void OnCustomerCreated(object sender, EventArgs e)
        {
            await GetCustomersAsync();
        }

        private async void OnCustomerDeleted(object sender, EventArgs e)
        {
            await GetCustomersAsync();
        }

        private async void OnCustomerUpdated(object sender, EventArgs e)
        {
            await GetCustomersAsync();
        }


        public async Task GetCustomersAsync()
        {
            var customers = await _customerService.GetCustomersAsync();

            var customerObservable = new ObservableCollection<CustomerItemsViewModel>();
            foreach( var customer in customers ) 
            {
                var customerDetail = new CustomerItemsViewModel(_updateCustomerViewModel, _deleteCustomerViewModel);
                customerDetail.Id = customer.Id;
                customerDetail.CustomerFullName = customer.CustomerFullName;
                customerDetail.CustomerBirthday = customer.CustomerBirthday;
                customerDetail.CustomerStatus = customer.CustomerStatus;
                customerDetail.Telephone = customer.Telephone;
                customerDetail.EmailAddress = customer.EmailAddress;
                customerObservable.Add(customerDetail);
            }

            Customers = customerObservable;
        }
    }
}
