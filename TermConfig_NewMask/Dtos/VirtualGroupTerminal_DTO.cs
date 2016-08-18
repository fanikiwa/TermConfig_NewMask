using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class VirtualGroupTerminal_DTO
    {
        public VirtualGroupTerminal_DTO()
        {
            this.ID = 0;
            this.TerminalID = 0;
            this.TerminalDescription = string.Empty;
            this.LocationNumber = 0;
            this.LocationDescription = string.Empty;
            this.CostCenterNumber = 0;
            this.CostCenterDescription = string.Empty;
            this.ConnectionType = "TCP/IP";
            this.Status = false;
            this.Action = false;
        }

        public long ID { get; set; }
        public long TerminalID { get; set; }
        public string TerminalDescription { get; set; }
        public long LocationNumber { get; set; }
        public string LocationDescription { get; set; }
        public long CostCenterNumber { get; set; }
        public string CostCenterDescription { get; set; }
        public string ConnectionType { get; set; }
        public bool Status { get; set; }
        public bool Action { get; set; }
        public string StatusString
        {
            get
            {
                string statusValue = string.Empty;

                var culture = HttpContext.Current.Session["PreferredCulture"].ToString();
                var cultureInfo = new CultureInfo(culture);

                switch (this.Status)
                {
                    case true:
                        statusValue = (string)HttpContext.GetGlobalResourceObject("LocalizedText", "statusActive", cultureInfo);
                        break;
                    case false:
                        statusValue = (string)HttpContext.GetGlobalResourceObject("LocalizedText", "statusInactive", cultureInfo);
                        break;
                }
                return statusValue;
            }
        }
    }
}