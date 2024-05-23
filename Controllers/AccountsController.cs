using Microsoft.AspNetCore.Mvc;
using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public AccountsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return _dataAccessProvider.GetAllAccounts();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Account account)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddAccount(account);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{username}")]
        public Account Details(string username)
        {
            return _dataAccessProvider.GetAccountByUserName(username);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Account account)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateAccount(account);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteConfirmed(string username)
        {
            var data = _dataAccessProvider.GetAccountByUserName(username);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteAccount(username);
            return Ok();
        }
    }
}
