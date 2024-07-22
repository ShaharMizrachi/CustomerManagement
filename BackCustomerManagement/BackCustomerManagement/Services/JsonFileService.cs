using System.Text.Json;
using BackCustomerManagement.Models;
using BackCustomerManagement.Interfaces;

namespace BackCustomerManagement.Services
{
    public class JsonFileService: IJsonFileService
    {
        private readonly string _filePath;

        public JsonFileService(string filePath)
        {
            _filePath = filePath;
        }

        public CustomerData GetCustomerData()
        {
            using (var jsonFileReader = File.OpenText(_filePath))
            {
                return JsonSerializer.Deserialize<CustomerData>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public void SaveCustomerData(CustomerData customerData)
        {
            // Open the file with FileMode.Create to overwrite the existing content
            using (var outputStream = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
            using (var writer = new Utf8JsonWriter(outputStream, new JsonWriterOptions
            {
                SkipValidation = true,
                Indented = true
            }))
            {
                JsonSerializer.Serialize(writer, customerData);
            }
        }


        public IEnumerable<Customer> GetCustomers()
        {
            var customerData = GetCustomerData();
            return customerData.Customers;
        }

        public void AddCustomer(Customer newCustomer)
        {
            var customerData = GetCustomerData();
            newCustomer.Id = customerData.Customers.Max(c => c.Id) + 1;
            customerData.Customers.Add(newCustomer);
            SaveCustomerData(customerData);
        }

        public void UpdateCustomer(Customer updatedCustomer)
        {
            var customerData = GetCustomerData();
            var customer = customerData.Customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);

            if (customer != null)
            {
                customer.Name = updatedCustomer.Name;
                customer.LastName = updatedCustomer.LastName;
                customer.Date = updatedCustomer.Date;
                customer.Phone = updatedCustomer.Phone;
                SaveCustomerData(customerData);
            }
            else
            {
                throw new KeyNotFoundException($"Customer with ID {updatedCustomer.Id} not found.");
            }
        }

        // Method to delete a customer
        public void DeleteCustomer(int customerId)
        {
            var customerData = GetCustomerData();
            var customer = customerData.Customers.FirstOrDefault(c => c.Id == customerId);

            if (customer != null)
            {
                customerData.Customers.Remove(customer);
                SaveCustomerData(customerData);
            }
            else
            {
                throw new KeyNotFoundException($"Customer with ID {customerId} not found.");
            }
        }

        public string GetVersion()
        {
            using (var jsonFileReader = File.OpenText(_filePath))
            {
                var customerData = JsonSerializer.Deserialize<CustomerData>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                return customerData?.Version;
            }
        }





    }
}
