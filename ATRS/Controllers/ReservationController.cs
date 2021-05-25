using ATRS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATRS.Controllers
{
    public class ReservationController : Controller
    {
        atrsEntities ctx = new atrsEntities();
        public ActionResult Reservations() 
        {
            var res = ctx.Database.SqlQuery<Reservation>("select * from Reservation").ToList<Reservation>();
            return View(res);
        }
        // GET: Reservation
        public ActionResult Book(Flight fDetials)
        {
            TempData["FlightNumber"] = fDetials.FlightNumber;
            TempData["FlightOrigin"] = fDetials.Origin;
            TempData["FlightDestination"] = fDetials.Destination;
            TempData["Layover"] = fDetials.Layover;
            var query = "select * from passenger";
            var res = ctx.Database.SqlQuery<Passenger>(query).ToList<Passenger>();
            return View(res);
        }

        [HttpPost]
        public ActionResult ConfirmBooking(FormCollection form) 
        {
            string flightNumber = form["FlightNumber"];
            string PassengerID = form["PassengerID"];
            string Ticket = form["Ticket"];
            var res = ctx.Database.SqlQuery<Reservation>("select * from Reservation order by ReservationNumber desc").ToList<Reservation>();
            var reservationID = res[0].ReservationNumber;
            reservationID += 1;
            var queryToReservation = ("insert into Reservation(ReservationNumber, PassengerID, Ticket, FlightNumber) values(" + reservationID + ",\'" + PassengerID + "\'," + "\'" + Ticket + "\'," + "\'" + flightNumber + "\')");
            ctx.Database.ExecuteSqlCommand(queryToReservation);
            ctx.SaveChanges();
            var resUpdated = ctx.Database.SqlQuery<Reservation>("select * from Reservation").ToList<Reservation>();
            return View("Reservations", resUpdated);
        }
    }
}
