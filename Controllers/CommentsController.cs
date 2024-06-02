using highlandcoffeeapp_BE.DataAccess;
using highlandcoffeeapp_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace highlandcoffeeapp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CommentsController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Comment> Get()
        {
            return _dataAccessProvider.GetAllComments();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddComment(comment);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public Comment Details(string id)
        {
            return _dataAccessProvider.GetCommentById(id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var existingComment = _dataAccessProvider.GetCommentById(id);
                if (existingComment == null)
                {
                    return NotFound();
                }

                comment.commentid = id;
                _dataAccessProvider.UpdateComment(comment);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(string id)
        {
            var data = _dataAccessProvider.GetCommentById(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteComment(id);
            return Ok();
        }
    }
}