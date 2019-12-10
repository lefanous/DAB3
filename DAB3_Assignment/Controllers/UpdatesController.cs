using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_Assignment.Models;
using DAB3_Assignment.Services;
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

        [HttpGet]
        public ActionResult<List<Update>> Feed()
        {
            List<Update> updates = _updateservice.Get();
            //updates.Sort((y, x) => x.CreationTime.CompareTo(y.CreationTime));

            //foreach (Update update in updates)
            //{
            //    update.Comments.Sort((y, x) => x.CreationTime.CompareTo(y.CreationTime));
            //}

            return View(updates);
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
            //_userservice.NewUpdate(update);

            return View("~/Views/Home/Contact.cshtml"/*, new { id = update.Author.ID }*/);
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