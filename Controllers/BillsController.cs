using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using System.Collections.Generic;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public BillsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Bill> Get()
        {
            return _dataAccessProvider.GetAllBills();
        }

        [HttpGet("{id}")]
        public Bill Get(string id)
        {
            return _dataAccessProvider.GetBillById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddBill(bill);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("bill/{orderid}")]
        public List<Bill> GetBillsByOrderId(string orderid)
        {
            return _dataAccessProvider.GetBillByOrderId(orderid);
        }

        [HttpGet("daily-revenue")]
        public IActionResult GetDailyRevenue([FromQuery] string date)
        {
            if (DateTime.TryParse(date, out DateTime parsedDate))
            {
                var result = _dataAccessProvider.GetDailyRevenue(parsedDate);
                return Ok(result);
            }
            return BadRequest("Invalid date format. Please use yyyy-MM-dd.");
        }


        [HttpGet("top-products")]
        public IActionResult GetTopProducts([FromQuery] DateTime date)
        {
            try
            {
                var result = _dataAccessProvider.GetTopProducts(date);
                if (result != null && result.Count > 0)
                {
                    return Ok(result);
                }
                return NotFound("No top products found for the specified date.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut]
        public IActionResult Put([FromBody] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateBill(bill);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("print")]
        public IActionResult PrintBill([FromBody] PrintBillRequest request)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.PrintBill(request.orderid, request.staffid);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var bill = _dataAccessProvider.GetBillById(id);
            if (bill != null)
            {
                _dataAccessProvider.DeleteBill(id);
                return Ok();
            }
            return NotFound();
        }
    }
}
