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
    public class TA_PersonalGroupMappingRepository: KruAllBaseRepository<TA_PersonalGroupMapping>
    {
        #region Constructor
        public TA_PersonalGroupMappingRepository() { }
        #endregion
        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TA_PersonalGroupMapping> GetAllPersonalGroupMapping()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TA_PersonalGroupMapping GetPersonalGroupById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        public List<TA_PersonalGroupMapping> GetPersonalGroupByGroupId(long groupId)
        {
            return base.FindBy(e => e.TerminalGroupId == groupId).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TA_PersonalGroupMapping GetPersonalInstance(long groupId, long personalNr)
        {
            return base.FindBy(e => e.TerminalGroupId == groupId && e.Pers_Nr == personalNr).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewPersonalGroupMapping(TA_PersonalGroupMapping personalGroupMapping)
        {
            base.Add(personalGroupMapping);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditPersonalGroupMapping(TA_PersonalGroupMapping personalGroupMapping)
        {
            if (personalGroupMapping.ID == 0) return;
            base.Edit(personalGroupMapping);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeletePersonalGroupMapping(TA_PersonalGroupMapping personalGroupMapping)
        {
            if (personalGroupMapping.ID == 0) return;
            var currentPersonalGroupMapping = GetPersonalGroupById(personalGroupMapping.ID);
            Delete(currentPersonalGroupMapping);
            Save();

        }

        #endregion
    }
}
