using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharpenTheSaw.Models;
using SharpenTheSaw.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SharpenTheSaw.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        public IActionResult Index(long? teamnameid, string teamname, int pageNum = 0)     //(string recipeSearch)
        {
            int pageSize = 5;
            //var blah = "%S%";
            return View(new IndexViewModel

            {
                Bowlers = (context.Bowlers
                .Where(t => t.TeamId == teamnameid || teamnameid == null)
                .OrderBy(t => t.BowlerLastName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList()),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,
                    
                    //If no team has been selected, then get the full count.
                    //Otherwise, only count the number from the meal type that has been selected.
                    TotalNumItems = (teamnameid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(x => x.TeamId == teamnameid).Count())
                },

                TeamCategory = teamname
            });



               


                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamnameid} OR {teamnameid} IS NULL")
                //// .FromSqlRaw("SELECT * FROM Bowlers WHERE BowlerLastName LIKE \"%S%\" ORDER BY BowlerLastName DESC")
                ////.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE BowlerLastName LIKE {recipeSearch} ORDER BY BowlerLastName DESC")
                //.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
