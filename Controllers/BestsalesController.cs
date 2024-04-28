using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class BestsalesController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public BestsalesController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<BestSale> Get()
        {
            return _dataAccessProvider.GetBestSalesRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] BestSale bestsale)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddBestSalesRecord(bestsale);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public BestSale Details(int id)
        {
            return _dataAccessProvider.GetBestSalesSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] BestSale bestsale)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateBestSalesRecord(bestsale);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetBestSalesSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteBestSalesRecord(id);
            return Ok();
    }
}
}
