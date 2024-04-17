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
            return _dataAccessProvider.GetCustomersRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddCustomersRecord(customer);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Customer Details(int id)
        {
            return _dataAccessProvider.GetCustomersSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Customer customer)
        {
            if (ModelState.IsValid)
            {   
                _dataAccessProvider.UpdateCustomersRecord(customer);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetCustomersSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCustomersRecord(id);
            return Ok();
        }
    }
}
