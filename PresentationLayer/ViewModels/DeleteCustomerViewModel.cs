using BusinessServiceLayer.Interfaces;
using PresentationLayer.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class DeleteCustomerViewModel : ViewModelBase
    {
        private readonly ICustomerService _customerService;

        public event EventHandler CustomerDeleted;

        public int Id { get; set; }

        public RelayCommand DeleteCustomerCommand { get; set; }

        public DeleteCustomerViewModel(ICustomerService customerService) 
        {
            _customerService = customerService;
            DeleteCustomerCommand = new RelayCommand(async o => await DeleteCustomer(o), o => true);
        }

        private async Task DeleteCustomer(object obj)
        {
           await _customerService.DeleteCustomerAsync(Id);
           CustomerDeleted?.Invoke(this, EventArgs.Empty);
        }

    }
}
