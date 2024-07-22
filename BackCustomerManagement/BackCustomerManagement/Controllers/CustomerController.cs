using Microsoft.AspNetCore.Mvc;
using BackCustomerManagement.Models;
using BackCustomerManagement.Services;
using System.Collections.Generic;
using BackCustomerManagement.Interfaces;

namespace BackCustomerManagement.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerController : ControllerBase
    {
        private readonly IJsonFileService _jsonFileService;

        public CustomerController(IJsonFileService jsonFileService)
        {
            _jsonFileService = jsonFileService;
        }

        // GET api/customers
        [HttpGet("Customers")]
        public ActionResult<IEnumerable<Customer>> GetlistOfCustomers(int customerVersion)
        {
            var customers = _jsonFileService.GetCustomers();
            var version = _jsonFileService.GetVersion();
            if (customerVersion != decimal.Parse(version) || version == null)
            {
                return Ok(customers);
            }
            else
            {
                return BadRequest("Version was not updated.");

            }
        }


        // get api/customer/{id}
        [HttpGet("Customer/{id}")]
        public ActionResult<Customer> GetbyId(int id)
        {
            var customers = _jsonFileService.GetCustomers();
            var customer = customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }




        // PUT api/customer/{id}
        [HttpPut("Customer/{id}")]
        public IActionResult EditCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            if (updatedCustomer == null || updatedCustomer.Id != id)
            {
                return BadRequest();
            }

            var existingCustomer = _jsonFileService.GetCustomers().FirstOrDefault(c => c.Id == id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            _jsonFileService.UpdateCustomer(updatedCustomer);
            return NoContent();
        }

        // DELETE api/customer/{id}
        [HttpDelete("Customer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var existingCustomer = _jsonFileService.GetCustomers().FirstOrDefault(c => c.Id == id);

            if (existingCustomer == null)
            {
                return NotFound();
            }

            _jsonFileService.DeleteCustomer(id);
            return NoContent();
        }

        [HttpGet("Customerslist/version")]
        public IActionResult GetVersion()
        {
            var version = _jsonFileService.GetVersion();
            if (version == null)
            {
                return NotFound("Version not found");
            }
            return Ok(version);
        }
    }
}
