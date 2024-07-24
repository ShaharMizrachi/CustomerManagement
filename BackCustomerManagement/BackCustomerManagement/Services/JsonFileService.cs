using System.Text.Json;
using BackCustomerManagement.Models;
using BackCustomerManagement.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BackCustomerManagement.Services
{
    public class JsonFileService: IJsonFileService
    {
        private readonly string _filePath;
        private readonly IPasswordHasher<Customer> _passwordHasher;

        public JsonFileService(string filePath, IPasswordHasher<Customer> passwordHasher)
        {
            _filePath = filePath;
            _passwordHasher = passwordHasher;
        }

        public CustomerData GetCustomersAndVersion()
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
            var customerData = GetCustomersAndVersion();
            return customerData.Customers;
        }

        public void AddCustomer(Customer newCustomer)
        {
            var customerData = GetCustomersAndVersion();
            newCustomer.Id = customerData.Customers.Max(c => c.Id) + 1;
            customerData.Customers.Add(newCustomer);
            SaveCustomerData(customerData);
        }

        public void UpdateCustomer(Customer updatedCustomer)
        {
            var customerData = GetCustomersAndVersion();
            var customer = customerData.Customers.FirstOrDefault(c => c.Id == updatedCustomer.Id);
            var newVersionTemp = (int.Parse(customerData.Version) + 1).ToString();
            if (customer != null)
            {
                customer.Name = updatedCustomer.Name;
                customer.LastName = updatedCustomer.LastName;
                customer.Date = updatedCustomer.Date;
                customer.Phone = updatedCustomer.Phone;
                customer.Email = updatedCustomer.Email;
                customer.Password = updatedCustomer.Password;
                customerData.Version = newVersionTemp;
                //if (!string.IsNullOrEmpty(updatedCustomer.Password))
                //{
                //   /* customer.Password = */HashUserPassword(customer);
                //}

                SaveCustomerData(customerData);
            }
            else
            {
                throw new KeyNotFoundException($"Customer with ID {updatedCustomer.Id} not found.");
            }
        }



        public void DeleteCustomer(int customerId)
        {
            var customerData = GetCustomersAndVersion();
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

        public void UpdatePasswords(IPasswordHasher<Customer> passwordHasher)
        {
            var customerData = GetCustomersAndVersion();
            foreach (var customer in customerData.Customers)
            {
                customer.Password = passwordHasher.HashPassword(customer, customer.Password);
            }
            SaveCustomerData(customerData);
        }

      
     public string HashUserPassword(Customer customer)
{
    if (customer == null || _passwordHasher == null)
        throw new ArgumentNullException("Customer or PasswordHasher cannot be null.");


    var hashedPassword = _passwordHasher.HashPassword(customer, customer.Password);
    return hashedPassword;
}






    }
}
