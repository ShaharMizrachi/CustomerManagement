using BackCustomerManagement.Models;

namespace BackCustomerManagement.Interfaces
{
    public interface IJsonFileService
    {
        CustomerData GetCustomerData();
        void SaveCustomerData(CustomerData customerData);
        IEnumerable<Customer> GetCustomers();
        void AddCustomer(Customer newCustomer);
        void UpdateCustomer(Customer updatedCustomer);
        void DeleteCustomer(int customerId);
        string GetVersion();
    }
}
