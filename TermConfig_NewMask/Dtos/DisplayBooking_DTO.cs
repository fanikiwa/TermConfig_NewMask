using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class DisplayBooking_DTO
    {
        public DisplayBooking_DTO()
        {
            this.ID = 0;
            this.Status1 = string.Empty;
            this.BookingTime1 = string.Empty;
            this.Status2 = string.Empty;
            this.BookingTime2 = string.Empty;
        }
        public int ID { get; set; }
        public string Status1 { get; set; }
        public string BookingTime1 { get; set; }
        public string Status2 { get; set; }
        public string BookingTime2 { get; set; }
    }
}