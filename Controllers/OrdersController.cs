using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public OrdersController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _dataAccessProvider.GetAllOrders();
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderDetail orderdetail)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddOrder(orderdetail);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("customer/{customerid}")]
        public List<Order> GetOrderByCustomerId(string customerid)
        {
            return _dataAccessProvider.GetOrderByCustomerId(customerid);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateOrder(order);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("confirm")]
        public IActionResult ConfirmOrder([FromBody] ConfirmOrderRequest request)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.ConfirmOrder(request.orderid, request.staffid);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("cancel/{id}")]
        public IActionResult CancelOrder(string id)
        {
            var data = _dataAccessProvider.GetOrderById(id);
            if (data == null)
            {
                return NotFound();
            }

            if (data.status != 0)
            {
                return BadRequest("Order cannot be cancelled as it is already confirmed or processed.");
            }

            _dataAccessProvider.CancelOrder(id);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetOrderById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteOrder(id);
            return Ok();
        }
    }
}
