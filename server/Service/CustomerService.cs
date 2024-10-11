using DataAccess;
using DataAccess.Models;
using Service.Interfaces;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly WebShopContext _context;

        public CustomerService(WebShopContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }
        
        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }
    }
}