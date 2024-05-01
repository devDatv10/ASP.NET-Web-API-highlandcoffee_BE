using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class FoodsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public FoodsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Food> Get()
        {
            return _dataAccessProvider.GetFoodsRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Food food)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddFoodsRecord(food);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Food Details(int id)
        {
            return _dataAccessProvider.GetFoodsSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Food food)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateFoodsRecord(food);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetFoodsSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteFoodsRecord(id);
            return Ok();
    }
}
}
