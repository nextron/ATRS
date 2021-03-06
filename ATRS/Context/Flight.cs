//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ATRS.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            this.Reservations = new HashSet<Reservation>();
            this.FlightTracks = new HashSet<FlightTrack>();
        }
    
        public string FlightNumber { get; set; }
        public string Aircraft { get; set; }
        public string Origin { get; set; }
        public string Layover { get; set; }
        public string Destination { get; set; }
        public string Airline { get; set; }
        public Nullable<System.DateTime> Startdatetime { get; set; }
        public Nullable<System.DateTime> Enddatetime { get; set; }
        public Nullable<int> Layoverdatetime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reservation> Reservations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FlightTrack> FlightTracks { get; set; }
    }
}
