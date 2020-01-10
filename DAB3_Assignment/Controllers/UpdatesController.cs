using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_Assignment.Models;
using DAB3_Assignment.Controllers;
using DAB3_Assignment.Services;
using DAB3_Assignment.SeedData;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DAB3_Assignment.Controllers
{
    public class UpdatesController : Controller
    {
        private readonly UpdateService _updateservice;
        private readonly UserService _userservice;

        public UpdatesController(UpdateService updateService, UserService userService)
        {
            _updateservice = updateService;
            _userservice = userService;
        }

        public ActionResult<List<Update>> Feed(string ID)
        {
            /* Get user which feed we are looking at */
            var user = _userservice.Get(ID);

            /* Get all circles that user is part of */
            var circles = user.Circles;
            //circles.Add("empty");            

            /* Get all updates */
            var updates = _updateservice.Get();

            /* Check updates that is visible for user through circles */
            var feedUpdates = new List<Update>();
            foreach (var circle in circles)
            {
                foreach (var update in updates)
                {
                    var author = _userservice.Get(update.Author.ID);


                    if (update.Circles == circle)
                    {
                        if (user.BlockedList.Count == 0)
                        {
                            var empty = new UserReference { ID = "empty", Name = "empty" };
                            user.BlockedList.Add(empty);
                        }

                        foreach (var blockeduser in user.BlockedList)
                        {     
                            if (author == null)
                            {
                                var person = new User { ID = "", Name = "", Age = 00, BlockedList = new List<UserReference>(), Circles = new List<string>(), Followers = new List<UserReference>(), Gender = "", Updates = new List<Update>()};
                                author = person;

                            }
                            if (blockeduser.ID != author.ID )
                            {
                                feedUpdates.Add(update);
                            }
                            
                        }
                    }
                }
                

            }
            foreach (var update in updates)
            {
                var author = _userservice.Get(update.Author.ID);
                if (update.Circles == "Public")
                {
                    if (user.BlockedList.Count == 0)
                    {
                        var empty = new UserReference { ID = "empty", Name = "empty" };
                        user.BlockedList.Add(empty);
                    }
                    foreach (var blockeduser in user.BlockedList)
                    {
                        if (blockeduser.ID != author.ID)
                        {
                            feedUpdates.Add(update);
                        }
                    }
                }
            }
            return View(feedUpdates);
        }
        public IActionResult VisitFeed()
        {
            var users = _userservice.Get();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Update update)
        {
            _updateservice.Create(update);
            var user = _userservice.Get(update.Author.ID);
            user.Updates.Add(update);



            return View("~/Views/Home/Contact.cshtml");
        }
        [HttpGet]
        public ActionResult<List<Update>> Get() =>
           _updateservice.Get();

        [HttpGet("{id:length(24)}", Name = "GetUpdates")]
        public Update Get(string id)
        {
            var update = _updateservice.Get(id);

            return update;
        }
    }
}