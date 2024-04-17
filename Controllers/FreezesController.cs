using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class FreezesController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public FreezesController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Freeze> Get()
        {
            return _dataAccessProvider.GetFreezesRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Freeze freeze)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddFreezesRecord(freeze);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Freeze Details(int id)
        {
            return _dataAccessProvider.GetFreezesSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Freeze freeze)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateFreezesRecord(freeze);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetFreezesSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteFreezesRecord(id);
            return Ok();
    }
}
}
