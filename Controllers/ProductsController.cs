using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
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
            return _dataAccessProvider.GetProductsRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                Guid obj = Guid.NewGuid();
                product.id = obj.CompareTo(Guid.NewGuid());
                _dataAccessProvider.AddProductsRecord(product);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Product Details(int id)
        {
            return _dataAccessProvider.GetProductsSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateProductsRecord(product);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetProductsSingleRecord(id);
            if (data == null)
            {
                return NotFound();
    }
            _dataAccessProvider.DeleteProductsRecord(id);
            return Ok();
        }
    }
}
