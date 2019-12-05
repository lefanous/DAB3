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
            var updates = _updateService.Get();
            return View(updates);
        }
    }
}