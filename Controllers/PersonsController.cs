using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public PersonsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _dataAccessProvider.GetAllPersons();
        }

        [HttpPost("access-role/{id}")]
        public IActionResult ActivateAccount(string id)
        {
            try
            {
                _dataAccessProvider.UpdateStaffToAdmin(id);
                return Ok("Account activated successfully");
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu cần
                return StatusCode(500, $"Error activating account: {ex.Message}");
            }
        }

        [HttpPost("cancel-role/{id}")]
        public IActionResult BlockAccount(string id)
        {
            try
            {
                _dataAccessProvider.UpdateAdminToStaff(id);
                return Ok("Account blocked successfully");
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu cần
                return StatusCode(500, $"Error blocking account: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Details(string id)
        {
            var person = _dataAccessProvider.GetRoleByPersonId(id);
            if (person != null)
            {
                return Ok(person);
            }
            return NotFound();
        }
    }
}