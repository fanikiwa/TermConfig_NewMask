using KruAll.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Models;
using System.ComponentModel;

namespace KruAll.Core.Repositories
{
    public class KruAllFingerPrintRepository : KruAllBaseRepository<FingerPrint>
    {
        //GetFingerPrint(persNo, fIndex, termArt)

        #region Constructor
        public KruAllFingerPrintRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]

        public FingerPrint GetFingerPrint(int persNo, int fIndex, string termArt, string termType)
        {
            return base.FindBy(f => f.PersNr == persNo && f.Findex == fIndex && f.TermArt == termArt && f.TermTyp == termType).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void CreateNewUserFingerPrintData(FingerPrint fingerPrintData)
        {
            base.Add(fingerPrintData);
            Save();
        }
        #endregion Methods
    }
}
