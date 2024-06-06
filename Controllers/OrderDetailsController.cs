using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public OrderDetailsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<OrderDetail> Get()
        {
            return _dataAccessProvider.GetAllOrderDetails();
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddOrderDetail(orderDetail);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("order/{orderid}")]
        public List<OrderDetail> GetOrderDetailByOrderId(string orderid)
        {
            return _dataAccessProvider.GetOrderDetailByOrderId(orderid);
        }


        [HttpPut]
        public IActionResult Edit([FromBody] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateOrderDetail(orderDetail);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetOrderDetailById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteOrderDetail(id);
            return Ok();
        }
    }
}