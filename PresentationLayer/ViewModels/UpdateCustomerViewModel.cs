﻿using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModels
{
    public class UpdateCustomerViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public event EventHandler CustomerUpdated;
        public RelayCommand UpdateCustomerCommand { get; set; }

        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CustomerBirthday { get; set; }
        public bool CustomerStatus { get; set; }
        public string Password { get; set; }

        public UpdateCustomerViewModel(ICustomerService customerService, IMapper mapper, IUserService userService) 
        {
            _customerService = customerService;
            _mapper = mapper;
            _userService = userService;
            UpdateCustomerCommand = new RelayCommand(async o => await UpdateCustomerAsync(o), CanExecuteUpdateCustomerCommand);
        }

        public async Task LoadCustomerDetail(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer != null)
            {
                CustomerId = customer.Id;
                CustomerFullName = customer.CustomerFullName;
                Telephone = customer.Telephone;
                EmailAddress = customer.EmailAddress;
                CustomerBirthday = customer.CustomerBirthday;
                CustomerStatus = customer.CustomerStatus;
            }
        }

        private bool CanExecuteUpdateCustomerCommand(object obj)
        {
            if (
                string.IsNullOrWhiteSpace(CustomerFullName) ||
                string.IsNullOrWhiteSpace(Telephone) ||
                string.IsNullOrWhiteSpace(EmailAddress) ||
                !DateTime.TryParse(CustomerBirthday.ToString(), out DateTime parsedBirthday) ||
                string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            return true;
        }

        private async Task UpdateCustomerAsync(object obj)
        {
            var oldCustomer = await _customerService.GetCustomerByIdAsync(CustomerId);
            bool emailExisted = await _userService.CheckEmailExisted(EmailAddress);
            bool phoneExisted = await _userService.CheckPhoneExisted(Telephone);
            if (EmailAddress != oldCustomer.EmailAddress && emailExisted)
            {
                MessageBox.Show("Email existed!");
            }
            else if (Telephone != oldCustomer.Telephone && phoneExisted)
            {
                MessageBox.Show("Phone existed!");
            }
            else
            {
                var customerToUpdate = _mapper.Map<UpdateCustomerViewModel, CustomerToAddOrUpdateDTO>(this);
                await _customerService.UpdateCustomerAsync(customerToUpdate, CustomerId);
                MessageBox.Show("Customer Updated Successfully");
                CustomerUpdated?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
