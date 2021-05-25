using ATRS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATRS.Controllers
{
    public class FlightController : Controller
    {
        // GET: Flight
        atrsEntities ctx = new atrsEntities();

        public ActionResult FlightSearch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FlightList(Flight model)
        {
            var date = model.Startdatetime.ToString();
            var dateInput = Convert.ToDateTime(date).ToString("dd-MMMM-yyyy");
            var query = "select * from flight where lower(Origin) = " + "\'" + model.Origin.ToLower() + "\'" + " and lower(Destination) = " + "\'" + model.Destination.ToLower() + "\'" +" and Startdatetime between dateadd(day, -1, " + "\'" + dateInput + "\'" + ") and dateadd(day, 1, " + "\'" + dateInput + "\'" + ")" +" order by Layoverdatetime";
            var res = ctx.Database.SqlQuery<Flight>(query).ToList<Flight>();
            return View(res);
        }
        
    }
}