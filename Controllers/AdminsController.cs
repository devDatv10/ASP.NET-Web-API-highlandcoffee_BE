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
            return _dataAccessProvider.GetAllAdmins();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Admin admin)
        {if (ModelState.IsValid)
            {
                _dataAccessProvider.AddAdmin(admin);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Admin Details(string id)
        {
            return _dataAccessProvider.GetAdminById(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateAdmin(admin);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetAdminById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteAdmin(id);
            return Ok();
        }
    }
}
