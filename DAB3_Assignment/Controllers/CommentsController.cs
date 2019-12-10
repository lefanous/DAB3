using DAB3_Assignment.Models;
using DAB3_Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DAB3_Assignment.Controllers
{

    public class CommentsController : Controller
    {
        private readonly CommentService _commentservice;
        private readonly UpdateService _updateservice;

        public CommentsController(CommentService commentservice, UpdateService updateservice)
        {
            _commentservice = commentservice;
            _updateservice = updateservice;
        }

        [HttpGet]
        public ActionResult<List<Comment>> Get() =>
            _commentservice.Get();

        [HttpGet("{id:length(24)}", Name = "GetComments")]
        public ActionResult<Comment> Get(string id)
        {
            var comment = _commentservice.Get(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Comment comment)
        {
            _commentservice.Create(comment);
            _updateservice.NewComment(comment);

            return View("~/Views/Home/Contact.cshtml"/*, new { id = update.Author.ID }*/);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Comment commentIn)
        {
            var comment = _commentservice.Get(id);

            if (comment == null)
            {
                return NotFound();
            }

            _commentservice.Update(id, commentIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var comment = _commentservice.Get(id);

            if (comment == null)
            {
                return NotFound();
            }

            _commentservice.Remove(comment.ID);

            return NoContent();
        }
    }
}