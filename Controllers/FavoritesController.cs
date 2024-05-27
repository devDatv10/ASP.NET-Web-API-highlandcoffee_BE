using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public FavoritesController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Favorite> Get()
        {
            return _dataAccessProvider.GetAllFavorites();
        }

        [HttpGet("{id}")]
        public Favorite Details(string id)
        {
            return _dataAccessProvider.GetFavoriteById(id);
        }

        [HttpGet("customer/{customerid}")]
        public IEnumerable<Favorite> GetByCustomerId(string customerid)
        {
            return _dataAccessProvider.GetFavoritesByCustomerId(customerid);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddFavorite(favorite);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetFavoriteById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteFavorite(id);
            return Ok();
        }
    }
}