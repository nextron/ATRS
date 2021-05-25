using ATRS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATRS.Models;

namespace ATRS.Controllers
{
    public class HomeController : Controller
    {
        atrsEntities ctx = new atrsEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PFDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PFDetailsView(FormCollection form)
        {
            var reservationNumber = form["ReservationNumber"];
            var reservationResult = ctx.Database.SqlQuery<Reservation>("Select * from Reservation where ReservationNumber=" + reservationNumber).ToList<Reservation>();
            var passengerID = reservationResult[0].PassengerID;
            var flightNumber = reservationResult[0].FlightNumber;
            var passengerResult = ctx.Database.SqlQuery<Passenger>("select * from Passenger where PassengerID=" + passengerID).ToList<Passenger>();
            var flightResult = ctx.Database.SqlQuery<Flight>("Select * from Flight where FlightNumber=\'" + flightNumber+"\'").ToList<Flight>();
            TempData["PName"] = passengerResult[0].PassengerName;
            TempData["Ppassport"] = passengerResult[0].PassportNo;
            TempData["PMobile"] = passengerResult[0].MobileNo;
            TempData["FName"] = flightResult[0].Airline;
            TempData["FSource"] = flightResult[0].Origin;
            TempData["FDestination"] = flightResult[0].Destination;
            TempData["FStart"] = flightResult[0].Startdatetime;
            TempData["FLayover"] = "None";
            if (flightResult[0].Layover != null) 
            { 
                TempData["FLayover"] = flightResult[0].Layover;
            }
            //var travelTime = (flightResult[0].Enddatetime - flightResult[0].Startdatetime);
            //var totalHours = travelTime.TotalHours;
            TempData["JourneyTime"] = flightResult[0].Enddatetime - flightResult[0].Startdatetime;
            TempData["id"] = reservationNumber;
            return View();
        }

        public ActionResult TravelHistory() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult TravelHistoryView(FormCollection form) 
        {
            var passsengerName = form["PassengerName"];
            var passportNo = form["PassportNo"];
            //TempData["passport"] = passportNo;
            var query = @"Select Passenger.PassengerID ,Passenger.PassengerName,Passenger.PassportNo,Reservation.Ticket,
                        Flight.FlightNumber,Flight.Origin,Flight.Destination
                        from Passenger inner join Reservation on Passenger.PassengerID = Reservation.PassengerID
                        inner join Flight on Reservation.FlightNumber = Flight.FlightNumber
                        where Passenger.PassengerName = '" + passsengerName +"\'" + "and PassportNo = '" +passportNo + "\'";
            var res = ctx.Database.SqlQuery<TravelHistory>(query).ToList<TravelHistory>();
            return View(res);
        }

        public ActionResult TotalAicraft() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult TotalAicraftView(FormCollection form) 
        {
            var airLine = form["AirLineName"];
            var query = "select * from flight where lower(airline) = \'" +airLine.ToLower()+"\'";
            var res = ctx.Database.SqlQuery<Flight>(query).ToList<Flight>();
            TempData["countOfAirline"] = res.Count;
            TempData["airLineName"] = airLine;
            return View();
        }
    }
}
