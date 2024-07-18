using AutoMapper;
using BusinessServiceLayer.DTOs;
using BusinessServiceLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Specifications;

namespace BusinessServiceLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<bool> CheckEmailExisted(string email)
        {
            var user = await GetUserByEmailAsync(email);
            if (user != null) return true;
            return false;
        }

        public async Task<bool> CheckPhoneExisted(string phoneNumber)
        {
            var spec = new CustomerSpecification(phoneNumber);
            var user = await _customerRepository.GetEntityWithSpec(spec);
            if (user != null) return true;
            return false;
        }

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            var spec = new AuthenticationSpecification(email);
            var user = await _customerRepository.GetEntityWithSpec(spec);
            if (user == null)
            {
                return null;
            }
            return _mapper.Map<Customer, UserDTO>(user);
        }

        public async Task<UserDTO> LoginAsync(string email, string password)
        {
            var spec = new AuthenticationSpecification(email, password);
            var user = await _customerRepository.GetEntityWithSpec(spec);
            if(user == null)
            {
                return null;
            }
            return _mapper.Map<Customer, UserDTO>(user);
        }
    }
}
