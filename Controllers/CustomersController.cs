using BankingAPI.Data;
using BankingAPI.Data.Interfaces;
using BankingAPI.Data.Repo;
using BankingAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
              
        private readonly IUnitOfWork uow;
       
        public CustomersController(IUnitOfWork uow)
        {
            
            this.uow = uow;
            
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCustomers()
        {
            
            var customers = await uow.CustomerRepository.GetCustomersAsync();
            return Ok(customers);
             
        }
        //post api/customers/addcustomer
        [HttpPost("addcustomer")]
        public async Task<IActionResult> AddCustomer(Customers customer)
        {
            if (await uow.CustomerRepository.CustomerAlreadyExist(customer.NationalID))
                return BadRequest("Customer with the same NationalID Already exists");
            await uow.CustomerRepository.AddCustomer(customer);
            await uow.SaveAsync();
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            uow.CustomerRepository.DeleteCustomer(id);
            await uow.SaveAsync();
            return StatusCode(200);
        }

        [HttpGet("findacustomer/{id}")]
        public async Task<IActionResult> FetchCustomerData(int id)
        {
            var customer = await uow.CustomerRepository.FindCustomer(id);
            return Ok(customer);
        }

       




    }
}
