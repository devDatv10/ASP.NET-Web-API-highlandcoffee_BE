using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public ProductsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _dataAccessProvider.GetAllProducts();
        }

        [HttpGet("category/{categoryid}")]
        public IEnumerable<Product> GetByCategory(string categoryid)
        {
            return _dataAccessProvider.GetProductsByCategoryId(categoryid);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddProduct(product);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Product Details(string id)
        {
            return _dataAccessProvider.GetProductById(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateProduct(product);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetProductById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteProduct(id);
            return Ok();
        }
    }
}