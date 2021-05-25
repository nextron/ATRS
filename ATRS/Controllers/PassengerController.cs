using ATRS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATRS.Controllers
{
    public class PassengerController : Controller
    {
        atrsEntities ctx = new atrsEntities();
        // GET: Passenger
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PassengerList() 
        {
            var res = ctx.Database.SqlQuery<Passenger>("select * from passenger").ToList<Passenger>();
            return View(res);
        }
        public ActionResult AddPassenger(Passenger passenger) 
        {
            if (passenger != null) 
            {
                return View(passenger);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddPassengerToDB(Passenger passenger) 
        {
            if (passenger.PassengerID > 0)
            {
                var updateQuery = ("update Passenger set PassengerName =" + "\'" + passenger.PassengerName + "\'," + "PassportNo = '" + passenger.PassportNo + "\' ,MobileNo = " + passenger.MobileNo + "where PassengerID =" + passenger.PassengerID);
                ctx.Database.ExecuteSqlCommand(updateQuery);
                ctx.SaveChanges();
            }
            else
            { 
                var res = ctx.Database.SqlQuery<Passenger>("select * from passenger order by PassengerID desc").ToList<Passenger>();
                var passengerID = res[0].PassengerID + 1 ;
                var query = ("insert into passenger(PassengerID, PassengerName, PassportNo, MobileNo) values(" + passengerID + ",\'" + passenger.PassengerName + "\',\'" + passenger.PassportNo + "\'," + passenger.MobileNo + ")");
                ctx.Database.ExecuteSqlCommand(query);
                ctx.SaveChanges();
            }
            var resUpdated = ctx.Database.SqlQuery<Passenger>("select * from passenger").ToList<Passenger>();
            return View("PassengerList", resUpdated);
        }
        public ActionResult PassengerHistory()
        {
            return View();
        }
        public ActionResult UpdatePassenger() 
        { 
            return View(); 
        }
        public ActionResult DeletePassenger(int PassengerID) 
        {
            var query = ("delete from passenger where PassengerID=" + PassengerID);
            ctx.Database.ExecuteSqlCommand(query);
            ctx.SaveChanges();
            var resUpdated = ctx.Database.SqlQuery<Passenger>("select * from passenger").ToList<Passenger>();
            return View("PassengerList", resUpdated);
        }
    }
}
