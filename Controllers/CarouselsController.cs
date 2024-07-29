using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarouselsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CarouselsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Carousel> Get()
        {
            return _dataAccessProvider.GetAllCarousels();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Carousel carousel)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddCarousel(carousel);
                return Ok();
            }
            return BadRequest();
        }

        // [HttpGet("{id}")]
        // public Product Details(string id)
        // {
        //     return _dataAccessProvider.GetProductById(id);
        // }

        // [HttpPut("{id}")]
        // public IActionResult Update(string id, [FromBody] Product product)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var existingProduct = _dataAccessProvider.GetProductById(id);
        //         if (existingProduct == null)
        //         {
        //             return NotFound();
        //         }

        //         product.productid = id;
        //         _dataAccessProvider.UpdateProduct(product);
        //         return Ok();
        //     }
        //     return BadRequest();
        // }

        // [HttpDelete("{id}")]
        // public IActionResult DeleteConfirmed(string id)
        // {
        //     var data = _dataAccessProvider.GetProductById(id);
        //     if (data == null)
        //     {
        //         return NotFound();
        //     }
        //     _dataAccessProvider.DeleteProduct(id);
        //     return Ok();
        // }
    }
}