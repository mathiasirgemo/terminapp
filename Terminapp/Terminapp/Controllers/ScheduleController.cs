using Microsoft.AspNetCore.Mvc;
using Terminapp.Services;
using System;

namespace Terminapp.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly ScheduleService _scheduleService;

        public SchedulesController(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Schedules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DateTime startDate, DateTime endDate)
        {
            if (ModelState.IsValid)
            {
                var schedule = _scheduleService.GenerateSchedule(startDate, endDate);
                return RedirectToAction(nameof(Index)); // Redirect til index som viser terminlister
            }

            return View(); // Hvis det er en feil, gå tilbake til view
        }

        // GET: Schedules/Index
        public IActionResult Index()
        {
            var schedules = _scheduleService.GetAllSchedules(); // Du kan implementere denne metoden
            return View(schedules);
        }
    }
}