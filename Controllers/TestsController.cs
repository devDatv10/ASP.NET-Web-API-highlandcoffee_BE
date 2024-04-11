using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public TestsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Test> Get()
        {
            return _dataAccessProvider.GetTestsRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Test test)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                test.id = obj.CompareTo(Guid.NewGuid());
                _dataAccessProvider.AddTestsRecord(test);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Test Details(int id)
        {
            return _dataAccessProvider.GetTestsSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Test test)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateTestsRecord(test);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetTestsSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteTestsRecord(id);
            return Ok();
        }
    }
}