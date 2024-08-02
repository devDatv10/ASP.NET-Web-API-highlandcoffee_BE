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

        [HttpPut("{id}/cancel")]
        public IActionResult Cancel(string id)
        {
            try
            {
                var existingCarousel = _dataAccessProvider.GetCarouselById(id);
                if (existingCarousel == null)
                {
                    return NotFound();
                }

                _dataAccessProvider.CancelCarousel(id);
                return Ok("Carousel cancelled successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}/activate")]
        public IActionResult Activate(string id)
        {
            try
            {
                var existingCarousel = _dataAccessProvider.GetCarouselById(id);
                if (existingCarousel == null)
                {
                    return NotFound();
                }

                _dataAccessProvider.ActivateCarousel(id);
                return Ok("Carousel activated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var existingCarousel = _dataAccessProvider.GetCarouselById(id);
                if (existingCarousel == null)
                {
                    return NotFound();
                }

                _dataAccessProvider.DeleteCarousel(id);
                return Ok("Carousel deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}