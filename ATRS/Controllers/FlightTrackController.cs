using ATRS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATRS.Models;

namespace ATRS.Controllers
{
    public class FlightTrackController : Controller
    {
        atrsEntities ctx = new atrsEntities();
        // GET: FlightTrack
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MostVisitedCity()
        {
            //ctx.MostVisitedCityGenerate();
            ViewBag.CityName = "No City Found";
            var query = "select TOP 1 FlightNumber, count(FlightNumber) as countFlight from FlightTrack where FlightDate > dateadd(month, -1, sysdatetime()) group by FlightNumber order by countFlight desc; ";
            var res = ctx.Database.SqlQuery<MostVisited>(query).ToList();
            if (res.Count > 0) { 
                var queryForCity = "select * from Flight where FlightNumber ="+"\'"+res[0].FlightNumber+"\'";
                var resCity = ctx.Database.SqlQuery<Flight>(queryForCity).ToList<Flight>();
                ViewBag.CityName = resCity[0].Destination;
            }
            return View();
        }
    }
}
