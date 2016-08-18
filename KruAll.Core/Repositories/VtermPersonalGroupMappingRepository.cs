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
    public class VtermPersonalGroupMappingRepository : KruAllBaseRepository<VirtualPersonalGroupsMapping>
    {
        #region Constructor
        public VtermPersonalGroupMappingRepository() { }
        #endregion
        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<VirtualPersonalGroupsMapping> GetAllPersonalGroupMapping()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public VirtualPersonalGroupsMapping GetPersonalGroupById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        public List<VirtualPersonalGroupsMapping> GetPersonalGroupByGroupId(long groupId)
        {
            return base.FindBy(e => e.VtermID == groupId).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public VirtualPersonalGroupsMapping GetPersonalInstance(long groupId, long personalNr)
        {
            return base.FindBy(e => e.VtermID == groupId && e.Pers_Nr == personalNr).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewPersonalGroupMapping(VirtualPersonalGroupsMapping personalGroupMapping)
        {
            base.Add(personalGroupMapping);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditPersonalGroupMapping(VirtualPersonalGroupsMapping personalGroupMapping)
        {
            if (personalGroupMapping.ID == 0) return;
            base.Edit(personalGroupMapping);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeletePersonalGroupMapping(VirtualPersonalGroupsMapping personalGroupMapping)
        {
            if (personalGroupMapping.ID == 0) return;
            var currentPersonalGroupMapping = GetPersonalGroupById(personalGroupMapping.ID);
            Delete(currentPersonalGroupMapping);
            Save();

        }

        #endregion
    }
}
