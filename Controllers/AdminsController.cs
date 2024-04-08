using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public AdminsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Admin> Get()
        {
            return _dataAccessProvider.GetAdminsRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Admin admin)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                admin.id = obj.CompareTo(Guid.NewGuid());
                _dataAccessProvider.AddAdminsRecord(admin);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Admin Details(int id)
        {
            return _dataAccessProvider.GetAdminsSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateAdminsRecord(admin);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetAdminsSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteAdminsRecord(id);
            return Ok();
        }
    }
}
