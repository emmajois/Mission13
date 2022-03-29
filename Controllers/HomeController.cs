using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {
       private IBowlersRepository _repo { get; set; }
       private ITeamsRepository _repoTeam { get; set; }

        public HomeController(IBowlersRepository temp, ITeamsRepository temp2)
        {
            _repo = temp;
            _repoTeam = temp2;
        }

        public IActionResult Index(int teamId)
        {
            //AKA if no filter is applied, teamId will be 0
            if (teamId == 0)
            {
                ViewBag.AllTeams = _repoTeam.Teams.ToList();
                ViewBag.TeamName = "Home";
                var datas = _repo.GetAll();
                return View(datas);
            }
            //The filtering happens here
            ViewBag.AllTeams = _repoTeam.Teams.ToList();
            var xyz = _repoTeam.Teams.FirstOrDefault(x => x.TeamId == teamId);
            ViewBag.TeamName = xyz.TeamName;
            var dataDiff = _repo.Bowlers.Where(x => x.TeamID == teamId).ToList();
            return View(dataDiff);

        }
        //The routes below all seem pretty self explainatory to me
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                _repo.Save(bowler);
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var x = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == id);

            return View(x);
        }

        [HttpPost]
        public IActionResult Edit(Bowler bowler)
        {
            var neededData = bowler.BowlerID;

            if (ModelState.IsValid)
            {
                _repo.Edit(bowler);
                return RedirectToAction("Index");
            }
            return View (neededData);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var x = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == id);
            _repo.Delete(x);
            return RedirectToAction("Index");
        }
    }
}
