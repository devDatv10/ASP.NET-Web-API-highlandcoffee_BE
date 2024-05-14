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
                _dataAccessProvider.AddProductsRecord(product);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{productid}")]
        public Product Details(string productid)
        {
            return _dataAccessProvider.GetProductsSingleRecord(productid);
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

        [HttpDelete("{productid}")]
        public IActionResult DeleteConfirmed(string productid)
        {
            var data = _dataAccessProvider.GetProductsSingleRecord(productid);
            if (data == null)
            {
                return NotFound();
    }
            _dataAccessProvider.DeleteProductsRecord(productid);
            return Ok();
        }
    }
}
