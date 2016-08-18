using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.ViewModels
{
    public class CommDBRawBookingsViewModel
    {
        #region Constructor
        public CommDBRawBookingsViewModel() { }

        #endregion

        #region Properties
        RawBookingsRepository commDBPersonalstammRepo = new RawBookingsRepository();
        #endregion

        #region Methods
        public List<RawBooking> GetAllRawBookings()
        {
            return commDBPersonalstammRepo.GetAllRawBookings().OrderByDescending(x => x.Date).ThenByDescending(x => x.Time).ToList();
        }

        public List<RawBooking> GetAllRawBookings(DateTime workingDate)
        {
            return commDBPersonalstammRepo.GetAllRawBookings(workingDate).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time).ToList();
        }
        #endregion
    }
}
