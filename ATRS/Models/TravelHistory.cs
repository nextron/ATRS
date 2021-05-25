using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATRS.Models
{
    public class TravelHistory
    {
        public int PassengerID { get; set; }
        public string PassengerName { get; set; }
        public string PassportNo { get; set; }
        public string Ticket { get; set; }
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}