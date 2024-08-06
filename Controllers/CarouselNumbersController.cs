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

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] CarouselNumber carouselnumber)
        {
            if (ModelState.IsValid)
            {
                var existingCarousel = _dataAccessProvider.GetCarouselNumberById(id);
                if (existingCarousel == null)
                {
                    return NotFound();
                }

                carouselnumber.settingid = id;
                _dataAccessProvider.UpdateCarouselNumber(carouselnumber);
                return Ok();
            }
            return BadRequest();
        }
    }
}