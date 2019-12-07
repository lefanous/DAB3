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
    public class CirclesController : Controller
    {
        private readonly CircleService _circleService;

        public CirclesController(CircleService circleService)
        {
            _circleService = circleService;
        }

        [HttpGet]
        public ActionResult<List<Circle>> Index()
        {
            var circle = _circleService.Get();
            return View(circle);
        }

        [HttpGet("{id:length(24)}", Name = "GetCircle")]
        public ActionResult<Circle> Get(string id)
        {
            var circle = _circleService.Get(id);

            if (circle == null)
            {
                return NotFound();
            }

            return circle;
        }

        [HttpPost]
        public ActionResult<Circle> Create(Circle circle)
        {
            _circleService.Create(circle);

            return CreatedAtRoute("GetCircle", new { id = circle.ID.ToString() }, circle);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Circle circleIn)
        {
            var circle = _circleService.Get(id);

            if (circle == null)
            {
                return NotFound();
            }

            _circleService.Update(id, circleIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var circle = _circleService.Get(id);

            if (circle == null)
            {
                return NotFound();
            }

            _circleService.Remove(circle.ID);

            return NoContent();
        }
    }
}
