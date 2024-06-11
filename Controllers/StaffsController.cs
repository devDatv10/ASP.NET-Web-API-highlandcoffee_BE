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
                _dataAccessProvider.AddStaff(staff);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Staff Details(string id)
        {
            return _dataAccessProvider.GetStaffById(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Staff staff)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _dataAccessProvider.GetStaffById(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                staff.staffid = id;
                _dataAccessProvider.UpdateStaff(staff);
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
            _dataAccessProvider.DeleteStaff(id);
            return Ok();
        }
    }
}