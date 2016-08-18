using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.Dtos
{
    public class CommDBBookingsDto
    {
        public CommDBBookingsDto()
        {
            this.ID = 0;
            this.PersNr = 0;
            this.PersAusweisNr = 0;
            this.PersName = string.Empty;
            this.Status = string.Empty;
            this.BookingTime = string.Empty;
            this.BookingDate = string.Empty;
            this.Terminal = string.Empty;
        }
        public long ID { get; set; }
        public int PersNr { get; set; }
        public long PersAusweisNr { get; set; }
        public string PersName { get; set; }
        public string Status { get; set; }
        public string BookingTime { get; set; }
        public string BookingDate { get; set; }
        public string Terminal { get; set; }
    }
}
