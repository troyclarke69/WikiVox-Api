using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService service)
        {
            _customerService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll([FromQuery] CustomerParams customerParams)
        {
            var Customers = await _customerService.GetAllAsync(customerParams);
            var meta = new
            {
                Customers.TotalCount,
                Customers.PageSize,
                Customers.CurrentPage,
                Customers.TotalPages,
                Customers.HasNext,
                Customers.HasPrevious
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(meta));
            return Ok(Customers);
        }

        [Route("[action]/{country}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllByCountry(string country)
        {
            var Customers = await _customerService.GetAllByCountryAsync(country);
            return Ok(Customers);
        }

        [HttpGet("{id:length(24)}", Name = "GetCustomer")]
        public async Task<ActionResult<Customer>> GetById(string id)
        {
            var Customer = await _customerService.GetByIdAsync(id);
            if (Customer == null)
            {
                return NotFound();
            }
            return Ok(Customer);
        }

        [Route("[action]/{name}")]
        [HttpGet]
        public async Task<ActionResult<Customer>> GetByCompanyName(string name)
        {
            var Customer = await _customerService.GetByNameAsync(name);
            if (Customer == null)
            {
                return NotFound();
            }
            return Ok(Customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer Customer)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _customerService.CreateAsync(Customer);
            return Ok(Customer);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Customer updatedCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedCustomer = await _customerService.GetByIdAsync(id);
            if (queriedCustomer == null)
            {
                return NotFound();
            }
            await _customerService.UpdateAsync(id, updatedCustomer);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Customer = await _customerService.GetByIdAsync(id);
            if (Customer == null)
            {
                return NotFound();
            }
            await _customerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
