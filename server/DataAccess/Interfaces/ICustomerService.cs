using DataAccess.Models;

namespace Service.Interfaces
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);
        Customer GetCustomerById(int id);
        
        IEnumerable<Customer> GetAllCustomers();
    }
}