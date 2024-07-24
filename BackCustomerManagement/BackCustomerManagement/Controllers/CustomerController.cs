using Microsoft.AspNetCore.Mvc;
using BackCustomerManagement.Models;
using BackCustomerManagement.Services;
using System.Collections.Generic;
using BackCustomerManagement.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BackCustomerManagement.Controllers
{
    [ApiController]
    [Route("api")]
    public class CustomerController : ControllerBase
    {
        private readonly IJsonFileService _jsonFileService;
        private readonly IPasswordHasher<Customer> _passwordHasher;

        public CustomerController(IJsonFileService jsonFileService, IPasswordHasher<Customer> passwordHasher)
        {
            _jsonFileService = jsonFileService;
            _passwordHasher = passwordHasher;
        }


        // GET api/customers
        [HttpGet("Customers")]
        public ActionResult<IEnumerable<Customer>> GetlistOfCustomers(int customerVersion)
        {
            var customers = _jsonFileService.GetCustomers();
            var version = _jsonFileService.GetVersion();
            if (customerVersion != decimal.Parse(version) || version == null)
            {
                return Ok(_jsonFileService.GetCustomersAndVersion());
            }
            else
            {
                return BadRequest("Version was not updated.");

            }
        }



        // GET api/customers
        [HttpPost("CustomerVerification")]
        public ActionResult<IEnumerable<Customer>> CustomerVerifucatikon([FromBody] Customer customerFromUser)
        {
            var customers = _jsonFileService.GetCustomers();
            
            var customer = customers.FirstOrDefault((c)=>c.Email == customerFromUser.Email && c.Password == customerFromUser.Password);

            if (customer == null)
            {
                return BadRequest("Access Denied");

            }
            else
            {
                return Ok("access granted");
            }
            //var hashPassResult = _passwordHasher.VerifyHashedPassword(customer, customer.Password, customerFromUser.Password);


            //if (hashPassResult == PasswordVerificationResult.Success)
            //{
            //    return Ok("access granted");
            //}
            //else
            //{
            //    return BadRequest("Access Denied");

            //}
        }



        // PUT api/customer/{id}
        [HttpPut("Customer")]
        public IActionResult EditCustomer([FromBody] Customer updatedCustomer)
        {
            if (updatedCustomer == null)
            {
                return BadRequest();
            }

            try
            {
                _jsonFileService.UpdateCustomer(updatedCustomer);
                return Ok("CUstomer has been edit");
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "An error occurred while updating the customer.");
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
