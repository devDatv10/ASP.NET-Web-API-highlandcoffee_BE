using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class Test1sController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public Test1sController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Test1> Get()
        {
            return _dataAccessProvider.GetTest1sRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Test1 test1)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddTest1sRecord(test1);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Test1 Details(string id)
        {
            return _dataAccessProvider.GetTest1sSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Test1 test1)
        {
            if (ModelState.IsValid)
            {   
                _dataAccessProvider.UpdateTest1sRecord(test1);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetTest1sSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteTest1sRecord(id);
            return Ok();
        }
    }
}