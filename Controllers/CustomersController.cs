using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CustomersController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _dataAccessProvider.GetAllCustomers();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddCustomer(customer);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Customer Details(string id)
        {
            return _dataAccessProvider.GetCustomerById(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _dataAccessProvider.GetCustomerById(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                customer.customerid = id;
                _dataAccessProvider.UpdateCustomer(customer);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetCustomerById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCustomer(id);
            return Ok();
        }
    }
}