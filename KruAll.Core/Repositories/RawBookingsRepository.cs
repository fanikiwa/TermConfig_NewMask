using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KruAll.Core.Repositories
{
    public class RawBookingsRepository : KruAllCommBaseRepository<RawBooking>
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<RawBooking> GetAllRawBookings()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<RawBooking> GetAllRawBookings(DateTime workingDate)
        {
            DateTime leastWorkingDate = workingDate.AddMinutes(-60);
            return base.GetAll().Where(x => x.Time >= leastWorkingDate).ToList() ?? new List<RawBooking>();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public RawBooking GetRawBookingsById(int id)
        {
            return base.FindBy(b => b.ID == id).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewRawBookings(RawBooking RawBookings)
        {
            base.Add(RawBookings);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditRawBookings(RawBooking RawBookings)
        {
            if (RawBookings.ID == 0) return;
            base.Edit(RawBookings);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteRawBookings(RawBooking RawBookings)
        {
            if (RawBookings.ID == 0) return;
            base.Delete(RawBookings);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteAllRawBookings()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        public void SaveRawBookings()
        {
            Save();
        }
    }
}
