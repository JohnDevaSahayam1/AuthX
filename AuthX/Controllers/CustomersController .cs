using AuthX.Application.Interfaces;
using AuthX.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthX.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IService<Customer> _customerService;

        public CustomersController(IService<Customer> customerService)
        {
            _customerService = customerService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
                return NotFound($"Customer with ID {id} not found");

            return Ok(customer);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCustomer = await _customerService.CreateAsync(customer);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdCustomer.Id },
                createdCustomer
            );
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != customer.Id)
                return BadRequest("Customer ID mismatch");

            var existingCustomer = await _customerService.GetByIdAsync(id);
            if (existingCustomer == null)
                return NotFound($"Customer with ID {id} not found");

            var updatedCustomer = await _customerService.UpdateAsync(customer);
            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _customerService.DeleteAsync(id);

            if (!deleted)
                return NotFound($"Customer with ID {id} not found");

            return NoContent();
        }
    }
}
