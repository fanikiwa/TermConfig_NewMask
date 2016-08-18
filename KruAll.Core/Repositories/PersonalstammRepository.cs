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
    public class PersonalstammRepository : KruAllCommBaseRepository<Personalstamm>
    {
        #region Constructors
        public PersonalstammRepository() { }
        public PersonalstammRepository(string connString)
        {
            if (connString.Trim() != "")
            {
                _contextKruAllComm.Database.Connection.ConnectionString = connString;
            }
        }
        #endregion

        #region Methods
        public List<Personalstamm> GetAllPersonal()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Personalstamm GetPersonalById(long id)
        {
            return base.FindBy(b => b.Pers_Nr == id).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewPersonal(Personalstamm Personal)
        {
            base.Add(Personal);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditPersonal(Personalstamm Personal)
        {
            if (Personal.Pers_Nr == 0) return;
            base.Edit(Personal);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeletePersonal(Personalstamm Personal)
        {
            if (Personal.Pers_Nr == 0) return;
            base.Delete(Personal);
            Save();
        }

        public void AddOrUpdatePersonal(Personalstamm Personal)
        {
            var commPersonal = base.GetAll().Where(x => x.Pers_Nr == Personal.Pers_Nr && x.Mandant == Personal.Mandant && x.TerminalGroup == Personal.TerminalGroup).FirstOrDefault();

            if (commPersonal == null)
            {
                Personal.CommStatus = base.GetAll().Max(x => x.CommStatus) + 1;

                if(Personal.CommStatus == null)
                {
                    Personal.CommStatus = 1;
                }

                Personal.CommAction = 1;
                base.Add(Personal);
            }
            else
            {
                if(commPersonal.Pers_Ausweis_Nr != Personal.Pers_Ausweis_Nr
                   || commPersonal.Pers_PinCode != Personal.Pers_PinCode 
                   || commPersonal.Pers_Name1 != Personal.Pers_Name1 
                   || commPersonal.Pers_Name2 != Personal.Pers_Name2)
                {
                    commPersonal.Pers_PinCode = Personal.Pers_PinCode;
                    commPersonal.Pers_Ausweis_Nr = Personal.Pers_Ausweis_Nr;
                    commPersonal.Pers_Name1 = Personal.Pers_Name1;
                    commPersonal.Pers_Name2 = Personal.Pers_Name2;
                    commPersonal.CommStatus = base.GetAll().Max(x => x.CommStatus) + 1;
                    commPersonal.CommAction = 1;
                    base.Save();
                }
            }
        }

        public void SaveAllPersonal()
        {
            Save();
        }

        #endregion
    }
}
