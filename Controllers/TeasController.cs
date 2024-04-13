using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class TeasController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public TeasController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Tea> Get()
        {
            return _dataAccessProvider.GetTeasRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tea tea)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                tea.id = obj.CompareTo(Guid.NewGuid());
                _dataAccessProvider.AddTeasRecord(tea);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Tea Details(int id)
        {
            return _dataAccessProvider.GetTeasSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Tea tea)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateTeasRecord(tea);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetTeasSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteTeasRecord(id);
            return Ok();
    }
}
}
