using RepositoryLayer.Entities;

namespace RepositoryLayer.Specifications
{
    public class AuthenticationSpecification : BaseSpecification<Customer>
    {
        public AuthenticationSpecification(string email) : base(x => x.EmailAddress == email)
        {

        }

        public AuthenticationSpecification(string email, string password) : 
            base(x => x.EmailAddress == email && x.Password == password && x.CustomerStatus == 1) 
        {
        }
    }
}
