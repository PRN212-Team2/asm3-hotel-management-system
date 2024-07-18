﻿using BusinessServiceLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using PresentationLayer.Commands;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PresentationLayer.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService _userService;
        private readonly MainViewModel _mainViewModel;
        private readonly IConfiguration _configuration;
        private readonly string _adminEmail;
        private readonly string _adminPassword;

        public string Email { get; set; }
        public string Password { get; set; }
        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
        private bool _isViewVisible = true;
        public bool IsViewVisible 
        {
            get 
            {
                return _isViewVisible;
            }
            set
            {
                _isViewVisible= value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        } 

        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(IUserService userService, MainViewModel mainViewModel, IConfiguration configuration) 
        {
            _userService = userService;
            _mainViewModel = mainViewModel;
            _configuration = configuration;
            _adminEmail = _configuration["AdminAccount:Email"];
            _adminPassword = _configuration["AdminAccount:Password"];
            LoginCommand = new RelayCommand(async o => await LoginAsync(o), CanExecuteLoginCommand);
        }

        // Determine if the user can login
        private bool CanExecuteLoginCommand(object obj)
        {
            if(string.IsNullOrEmpty(Email)) 
            {
                return false;
            }
            return true;
        }

        private async Task LoginAsync(object obj)
        {
            // Check if the email and password matches the admin's
            // email and password in appsettings.json file
            Password = ((PasswordBox) obj).Password;
            if(Email == _adminEmail && Password == _adminPassword)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(Email), [UserRole.Admin.ToString()]);
                IsViewVisible = false;
                await _mainViewModel.LoadCurrentUser();
            }
            else
            {
                // Create a new Principal with the currently authenticated user
                var user = await _userService.LoginAsync(Email, Password);
                if (user != null)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(
                        new GenericIdentity(Email), [UserRole.Customer.ToString()]);
                    IsViewVisible = false;
                    await _mainViewModel.LoadCurrentUser();
                }
                else
                {
                    ErrorMessage = "Invalid Email or Password";
                }
            }
        }
    }
}
