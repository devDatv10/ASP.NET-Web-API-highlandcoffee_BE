using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class BreadsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public BreadsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Bread> Get()
        {
            return _dataAccessProvider.GetBreadsRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Bread bread)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                bread.id = obj.CompareTo(Guid.NewGuid());
                _dataAccessProvider.AddBreadsRecord(bread);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Bread Details(int id)
        {
            return _dataAccessProvider.GetBreadsSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Bread bread)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateBreadsRecord(bread);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetBreadsSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteBreadsRecord(id);
            return Ok();
    }
}
}
