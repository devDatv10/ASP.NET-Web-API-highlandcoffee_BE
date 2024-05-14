using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CategoriesController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _dataAccessProvider.GetCategoriesRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddCategoriesRecord(category);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{categoryid}")]
        public Category Details(string categoryid)
        {
            return _dataAccessProvider.GetCategoriesSingleRecord(categoryid);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateCategoriesRecord(category);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{categoryid}")]
        public IActionResult DeleteConfirmed(string categoryid)
        {
            var data = _dataAccessProvider.GetCategoriesSingleRecord(categoryid);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCategoriesRecord(categoryid);
            return Ok();
        }
    }
}
