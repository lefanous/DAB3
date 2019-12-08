using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAB3_Assignment.Models;
using DAB3_Assignment.Services;
using Microsoft.AspNetCore.Mvc;

namespace DAB3_Assignment.Controllers
{
    public class UpdatesController : Controller
    {
        private readonly UpdateService _updateService;

        public UpdatesController(UpdateService updateService)
        {
            _updateService = updateService;
        }

        [HttpGet]
        public ActionResult<List<Update>> Feed()
        {
            List<Update> updates = _updateService.Get();
            updates.Sort((y, x) => x.CreationTime.CompareTo(y.CreationTime));

            foreach (Update update in updates)
            {
                update.Comments.Sort((y, x) => x.CreationTime.CompareTo(y.CreationTime));
            }

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
            _updateService.Create(update);
            return View("../Views/Users/Wall", new { id = update.Author.ID });
        }
    }
}