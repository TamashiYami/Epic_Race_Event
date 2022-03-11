using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class RacesController : Controller
    {
        // GET: Races
        public ActionResult Index()
        {
            var races = new List<Race>()
            {
                new Race("La course de ma vie", new DateTime(2022, 02, 14, 18,10, 00), 15, 20),
                new Race("Ma super pas course", new DateTime(2022, 02, 02, 22, 20, 22), 30, 18),
                new Race("Ma course pourrie", new DateTime(2022, 04, 02, 16, 15, 00), 50, 21),
            };

            var raceListViewModel = new RaceListViewModel(
                races,
                "Liste de courses"
            );

            return View("RaceList", raceListViewModel);
        }

        // GET: Races/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: Races/Create
        public ActionResult Create()
        {
            return View("CreateRace");
        }

        // POST: Races/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Race race)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction(nameof(Index));
                }
                // TODO: Add insert logic here
                return View("CreateRace");

            }
            catch
            {
                return View();
            }
        }

        // GET: Races/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Races/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Races/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Races/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}