using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class PopularsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public PopularsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Popular> Get()
        {
            return _dataAccessProvider.GetPopularsRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Popular popular)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                popular.id = obj.CompareTo(Guid.NewGuid());
                _dataAccessProvider.AddPopularsRecord(popular);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Popular Details(int id)
        {
            return _dataAccessProvider.GetPopularsSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Popular popular)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdatePopularsRecord(popular);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetPopularsSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeletePopularsRecord(id);
            return Ok();
        }
    }
}
