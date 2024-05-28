using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using System.Collections.Generic;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class CartDetailsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CartDetailsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<CartDetail> Get()
        {
            return _dataAccessProvider.GetAllCartDetails();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddCartDetail(cartDetail);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("customer/{customerid}")]
        public CartDetail GetCartDetailByCustomerId(string customerid)
        {
            return _dataAccessProvider.GetCartDetailByCustomerId(customerid);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateCartDetail(cartDetail);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string customerid)
        {
            var data = _dataAccessProvider.GetCartDetailByCustomerId(customerid);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCartDetailByCartId(customerid);
            return Ok();
        }
    }
}
