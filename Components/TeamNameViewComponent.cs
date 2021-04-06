using Microsoft.AspNetCore.Mvc;
using SharpenTheSaw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpenTheSaw.Components
{
    public class TeamNameViewComponent : ViewComponent 
    {
        private BowlingLeagueContext context;

        public TeamNameViewComponent(BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["teamname"];
            return View(context.Teams
                .Distinct()
                .OrderBy(x => x));


                //.Select(x => x.TeamName)
                //.Distinct()
                //.OrderBy(x => x)
                //.ToList());
        }
    }
}
