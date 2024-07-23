using BackCustomerManagement.Models;

namespace BackCustomerManagement.Interfaces
{
    public interface IJsonFileService
    {
        CustomerData GetCustomersAndVersion();
        void SaveCustomerData(CustomerData customerData);
        IEnumerable<Customer> GetCustomers();
        void AddCustomer(Customer newCustomer);
        void UpdateCustomer(Customer updatedCustomer);
        void DeleteCustomer(int customerId);
        string GetVersion();
    }
}
