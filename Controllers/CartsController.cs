using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CartsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return _dataAccessProvider.GetCartsRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddCartsRecord(cart);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Cart Details(string id)
        {
            return _dataAccessProvider.GetCartsSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateCartsRecord(cart);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetCartsSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCartsRecord(id);
            return Ok();
        }
    }
}
