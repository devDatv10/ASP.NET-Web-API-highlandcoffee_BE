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

        [HttpGet("{id}")]
        public Category Details(int id)
        {
            return _dataAccessProvider.GetCategoriesSingleRecord(id);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetCategoriesSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCategoriesRecord(id);
            return Ok();
        }
    }
}
