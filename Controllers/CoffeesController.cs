using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class CoffeesController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CoffeesController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Coffee> Get()
        {
            return _dataAccessProvider.GetCoffeesRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Coffee coffee)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddCoffeesRecord(coffee);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Coffee Details(int id)
        {
            return _dataAccessProvider.GetCoffeesSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Coffee coffee)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateCoffeesRecord(coffee);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetCoffeesSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCoffeesRecord(id);
            return Ok();
    }
}
}
