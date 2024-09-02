using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public StoresController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Store> Get()
        {
            return _dataAccessProvider.GetAllStore();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Store store)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddStoreInformation(store);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Store store)
        {
            if (ModelState.IsValid)
            {
                var existingStore = _dataAccessProvider.GetStoreInformationById(id);
                if (existingStore == null)
                {
                    return NotFound();
                }

                store.storeid = id;
                _dataAccessProvider.UpdateStoreInformation(store);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetStoreInformationById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteStoreInformation(id);
            return Ok();
        }
    }
}