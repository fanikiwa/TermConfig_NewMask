﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories.Base;
using KruAll.Core.Models;
using System.ComponentModel;

namespace KruAll.Core.Repositories
{
 public class FingerprintreadRepository :KruallPZEBaseRepository <PersonalStammeList>    
    {
     public FingerprintreadRepository()
     {

     }
     [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
     public List <PersonalStammeList>GetAllCellInfo()
     {
         return base.GetAll().ToList();
     }
     [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
     public void NewPersonal(PersonalStammeList PersonalStammeList)
     {
         base.Add(PersonalStammeList);
         Save();
     }

    }
}
