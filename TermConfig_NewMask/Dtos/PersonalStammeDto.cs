using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.Dtos
{
    public class PersonalStammeDto
    {
        #region Constructor
        public PersonalStammeDto() { }

        #endregion

        #region Properties
        public int Pers_Nr { get; set; }
        public string Pers_Name1 { get; set; }
        public string Pers_Name2 { get; set; }

        //Maps to fields in the Abteilungen table
        public int Abt_Nr { get; set; }
        public string Abt_Bezeichnung { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", Pers_Name1, Pers_Name2);}
        }
        #endregion
    }
}
