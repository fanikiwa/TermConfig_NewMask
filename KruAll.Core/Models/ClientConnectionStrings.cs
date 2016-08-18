using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;

namespace KruAll.Core.Models
{
    public static class ClientConnectionStrings
    {
        public static Dictionary<int, string> GetClientProviderConnectionStrings()
        {
            KruAllCommMandantRepositoy kruAllCommMandantRepo = new KruAllCommMandantRepositoy();
            //List<string> connectiionStrings = new List<string>();

            Dictionary<int, string> connectionStrings = new Dictionary<int, string>();


            var kruAllCommMandanten = kruAllCommMandantRepo.GetAllMandanten();

            foreach(var clientDB in kruAllCommMandanten)
            {
                SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
                stringBuilder.DataSource = clientDB.ServerPZE;
                stringBuilder.InitialCatalog = clientDB.DBPZE;
                stringBuilder.UserID = clientDB.UserDBPZE;
                stringBuilder.Password = clientDB.PwdDBPZE;
                connectionStrings.Add(Convert.ToInt32(clientDB.Man_Nummer), stringBuilder.ConnectionString + @";multipleactiveresultsets = True;application name = EntityFramework;persist security info = True");
            }

            return connectionStrings;
        }

        public static List<string> GetClientEntityConnectionStrings()
        {
            KruAllCommMandantRepositoy kruAllCommMandantRepo = new KruAllCommMandantRepositoy();
            List<string> connectiionStrings = new List<string>();

            const string CONNETION_STRING_KEY = "KrutecPZE_Entities";

            var kruAllCommMandanten = kruAllCommMandantRepo.GetAllMandanten();

            foreach (var clientDB in kruAllCommMandanten)
            {
                SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
                stringBuilder.DataSource = clientDB.ServerPZE;
                stringBuilder.InitialCatalog = clientDB.DBPZE;
                stringBuilder.UserID = clientDB.UserDBPZE;
                stringBuilder.Password = clientDB.PwdDBPZE;


                string entityConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[CONNETION_STRING_KEY].ConnectionString;
                var connectionStringBuilder =  new EntityConnectionStringBuilder(entityConnectionString);
                connectionStringBuilder.ProviderConnectionString = stringBuilder.ConnectionString;
                connectiionStrings.Add(connectionStringBuilder.ToString());
            }

            return connectiionStrings;
        }
    }
}
