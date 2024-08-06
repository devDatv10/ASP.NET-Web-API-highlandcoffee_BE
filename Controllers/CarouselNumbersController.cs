using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarouselNumbersController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CarouselNumbersController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<CarouselNumber> Get()
        {
            return _dataAccessProvider.GetAllNumberOfCarousels();
        }

        [HttpGet("carousel-settings")]
        public IActionResult GetCarouselSettings()
        {
            var numberOfCarousels = _dataAccessProvider.GetNumberOfCarousels();
            return Ok(new { number_of_carousels = numberOfCarousels });
        }
    }
}