using BusinessServiceLayer.DTOs;
using AutoMapper;
using RepositoryLayer.Interfaces;
using BusinessServiceLayer.Interfaces;
using RepositoryLayer.Entities;


namespace BusinessServiceLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;

        public CustomerService(IGenericRepository<Customer> customerRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _customerRepo = customerRepo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await _customerRepo.ListAllAsync();
            return _mapper.Map<IReadOnlyList<Customer>, IReadOnlyList<CustomerDTO>>(customers);
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(int id)
        {
            Customer customer = await _customerRepo.GetByIdAsync(id);
            if(customer == null) 
            {
                return null;
            }
            
            return _mapper.Map<Customer, CustomerDTO>(customer);
        }

        public async Task CreateCustomerAsync(CustomerToAddOrUpdateDTO customer)
        {
            if (customer == null) return;
            Customer customerToAdd = _mapper.Map<CustomerToAddOrUpdateDTO, Customer>(customer);
            _customerRepo.Add(customerToAdd);
            await _customerRepo.SaveAllAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            if (customer == null) return;
            customer.CustomerStatus = 0;
            _customerRepo.Update(customer);
            await _customerRepo.SaveAllAsync();
        }
        public async Task UpdateCustomerAsync(CustomerToAddOrUpdateDTO updatedCustomer, int id)
        {
            var customer = await _customerRepo.GetByIdAsync(id);
            if (customer == null) return;

            _mapper.Map(updatedCustomer, customer);

            _customerRepo.Update(customer);
            await _customerRepo.SaveAllAsync();
        }   
    }
}

