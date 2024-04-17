using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
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
            return _dataAccessProvider.GetFavoritesRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddFavoritesRecord(favorite);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Favorite Details(int id)
        {
            return _dataAccessProvider.GetFavoritesSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Favorite favorite)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateFavoritesRecord(favorite);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetFavoritesSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteFavoritesRecord(id);
            return Ok();
        }
    }
}
