using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class OthersController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public OthersController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Other> Get()
        {
            return _dataAccessProvider.GetOthersRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Other other)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                other.id = obj.CompareTo(Guid.NewGuid());
                _dataAccessProvider.AddOthersRecord(other);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Other Details(int id)
        {
            return _dataAccessProvider.GetOthersSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Other other)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateOthersRecord(other);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetOthersSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteOthersRecord(id);
            return Ok();
        }
    }
}
