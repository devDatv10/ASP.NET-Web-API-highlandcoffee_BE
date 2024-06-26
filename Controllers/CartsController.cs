﻿using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using System.Collections.Generic;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CartsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return _dataAccessProvider.GetAllCart();
        }

        [HttpPost]
        public IActionResult Create([FromBody] CartDetail cartDetail)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddCart(cartDetail);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("customer/{customerid}")]
        public Cart GetCartByCustomerId(string customerid)
        {
            return _dataAccessProvider.GetCartByCustomerId(customerid);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateCart(cart);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetCartById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCart(id);
            return Ok();
        }
    }
}
