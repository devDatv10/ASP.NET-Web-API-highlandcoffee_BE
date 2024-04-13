using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class CakesController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CakesController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Cake> Get()
        {
            return _dataAccessProvider.GetCakesRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cake cake)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                cake.id = obj.CompareTo(Guid.NewGuid());
                _dataAccessProvider.AddCakesRecord(cake);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Cake Details(int id)
        {
            return _dataAccessProvider.GetCakesSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Cake cake)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateCakesRecord(cake);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetCakesSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCakesRecord(id);
            return Ok();
    }
}
}
