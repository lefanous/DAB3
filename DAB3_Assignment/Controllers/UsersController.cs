using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_Assignment.Models;
using DAB3_Assignment.Services;
using Microsoft.AspNetCore.Mvc;
using DAB3_Assignment.SeedData;

namespace DAB3_Assignment.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly UpdateService _updateService;

        public UsersController(UserService userService, UpdateService updateService)
        {
            _userService = userService;
            _updateService = updateService;
        }

        [HttpGet]
        public ActionResult<List<User>> Index()
        {
            var users = _userService.Get();
            return View(users);
        }

        public IActionResult VisitWall(string ID)
        {
            User user = _userService.Get(ID);
            List<User> users = _userService.Get();

            users.Insert(0, user);

            return View(users);
        }

        public IActionResult Wall(string ID, string VisitorID)
        {
            User user = _userService.Get(ID);
            User visitor = _userService.Get(VisitorID);

            user.Updates = _updateService.Get(ID);
            user.Updates.Sort((y, x) => x.CreationTime.CompareTo(y.CreationTime));

            List<User> users = new List<User>
            {
                user,
                visitor
            };

            return View(users);
        }

        public IActionResult Follow(string ID, string VisitorID)
        {
            var user = _userService.Get(ID);
            var visitor = _userService.Get(VisitorID);

            _userService.AddFollower(user, visitor);            

            return RedirectToAction("Wall", new { ID, VisitorID });
        }

        public IActionResult Block(string ID, string VisitorID)
        {
            var user = _userService.Get(ID);
            var visitor = _userService.Get(VisitorID);

            // The block goes both ways!
            _userService.Block(user, visitor);
            _userService.Block(visitor, user);

            return RedirectToAction("Wall", new { ID, VisitorID });
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _userService.Create(user);

            return CreatedAtRoute("GetUser", new { id = user.ID.ToString() }, user);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, User userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.Remove(user.ID);

            return NoContent();
        }
    }
}