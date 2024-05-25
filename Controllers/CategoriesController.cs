using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return _dataAccessProvider.GetAllCategories();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddCategory(category);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Category Details(string id)
        {
            return _dataAccessProvider.GetCategoryById(id);
        }

        [HttpPut("{id}")]
        public IActionResult Edit([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateCategory(category);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var category = _dataAccessProvider.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            _dataAccessProvider.DeleteCategory(id);
            return Ok();
        }
    }
}