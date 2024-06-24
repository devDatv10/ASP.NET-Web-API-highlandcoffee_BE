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

        [HttpGet("sizes/{productname}")]
        public IActionResult GetProductSizes(string productname)
        {
            try
            {
                var sizes = _dataAccessProvider.GetPriceBySize(productname);
                return Ok(sizes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get sizes and prices: {ex.Message}");
            }
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

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _dataAccessProvider.GetProductById(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                product.productid = id;
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