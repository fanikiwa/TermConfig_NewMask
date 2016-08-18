using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories;
using KruAll.Core.Models;

namespace KruAll.Core.Models
{
    public class MandantenGet
    {
        public void GetMandantenFromKruSysDatabase()
        {
            KruSysMandantRepositoy kruSysMandantRepo = new KruSysMandantRepositoy();
            KruAllCommMandantRepositoy kruAllCommMandantRepo = new KruAllCommMandantRepositoy();

            var kruSysMandanten = kruSysMandantRepo.GetAllMandanten();

            kruAllCommMandantRepo.DeleteAllMandanten();

            foreach (var mandant in kruSysMandanten)
            {
                var newMandant = new CommMandanten();
                newMandant.ID = mandant.ID;
                newMandant.Man_Nummer = mandant.Man_Nummer ?? 0;
                newMandant.Man_Firma1 = mandant.Man_Firma1;
                newMandant.Man_Firma2 = mandant.Man_Firma2;
                newMandant.Man_Strasse = mandant.Man_Strasse;
                newMandant.Man_Plz = mandant.Man_Plz;
                newMandant.Man_Ort = mandant.Man_Ort;
                newMandant.Man_Postfach = mandant.Man_Postfach;
                newMandant.Man_Telefon1 = mandant.Man_Telefon1;
                newMandant.Man_Telefax = mandant.Man_Telefax;
                newMandant.Man_Telefon2 = mandant.Man_Telefon2;
                newMandant.Aktiv = mandant.Aktiv;
                newMandant.Info = mandant.Info;
                newMandant.ServerPZE = mandant.ServerPZE;
                newMandant.DBPZE = mandant.DBPZE;
                newMandant.ServerAZE = mandant.ServerAZE;
                newMandant.DBAZE = mandant.DBAZE;
                newMandant.UserDBPZE = mandant.UserDBPZE;
                newMandant.PwdDBPZE = mandant.PwdDBPZE;
                newMandant.UserDBAZE = mandant.UserDBAZE;
                newMandant.PwdDBAZE = mandant.PwdDBAZE;
                kruAllCommMandantRepo.AddNewMandanten(newMandant);
            }

            kruAllCommMandantRepo.SaveMandanten();
        }

        public void dd()
        {
            GetMandantenFromKruSysDatabase();
        }
    }
}
