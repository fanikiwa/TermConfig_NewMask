using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories.Base;
using KruAll.Core.Models;
using System.ComponentModel;
namespace KruAll.Core.Repositories
{
    public class vwAccessProfileRepository : KruAllBaseRepository<RV_AccessProfilesPerTerminal>
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<RV_AccessProfilesPerTerminal> GetvwAccessProfiles()
        {
            return base.GetAll().ToList();
        }

        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public vwAccessProfile GetvwAccessProfileById(long id)
        //{
        //    return base.FindBy(b => b.Id == id).Include(p => p.vwAccessProfileMonths).FirstOrDefault();
        //}

        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public vwAccessProfile GetvwAccessProfileByNumber(string calendarNumber)
        //{
        //    return base.FindBy(b => b.vwAccessProfileNumber == calendarNumber).Include(p => p.vwAccessProfileMonths).FirstOrDefault();
        //}

        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public vwAccessProfile GetvwAccessProfileByCalendarYear(int calendarYear)
        //{
        //    return base.FindBy(b => b.CalendarYear == calendarYear).Include(p => p.vwAccessProfileMonths).FirstOrDefault();
        //}

        //[DataObjectMethod(DataObjectMethodType.Insert, true)]
        //public void NewvwAccessProfile(vwAccessProfile vwAccessProfile)
        //{
        //    base.Add(vwAccessProfile);
        //    Save();
        //}

        //[DataObjectMethod(DataObjectMethodType.Update, true)]
        //public void EditvwAccessProfile(vwAccessProfile switchCalendar)
        //{
        //    if (switchCalendar.Id == 0) return;
        //    base.Edit(switchCalendar);
        //    Save();
        //}

        //[DataObjectMethod(DataObjectMethodType.Delete, true)]
        //public void DeletevwAccessProfile(vwAccessProfile switchCalendar)
        //{
        //    if (switchCalendar.Id == 0) return;
        //    var currentDailyCalendar = GetvwAccessProfileById(switchCalendar.Id);
        //    base.Delete(currentDailyCalendar);

        //    Save();
        //}
    }
}
