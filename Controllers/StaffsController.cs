using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class StaffsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public StaffsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Staff> Get()
        {
            return _dataAccessProvider.GetAllStaffs();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddStaffRecord(staff);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Staff Details(string id)
        {
            return _dataAccessProvider.GetStaffById(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateStaffRecord(staff);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetStaffById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteStaffRecord(id);
            return Ok();
        }
    }
}