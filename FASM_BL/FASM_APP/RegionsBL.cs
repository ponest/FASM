using FASM_DAL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BL.Setups
{
    public class RegionsBL
    {
        private RegionsDAL RegionsDAL = new RegionsDAL();

        public DataTable GetRegions()
        {
            return RegionsDAL.GetRegions();
        }

        public void LoadRegions(ref Regions eRegions)
        {
            RegionsDAL.LoadRegions(ref eRegions);
        }

        public FASM_Enums.InfoMessages SaveRegions(ref Regions eRegions)
        {
            //Check if already exists
            if (RegionsDAL.DoesRegionsExists(eRegions.RegionId, eRegions.RegionName) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eRegions.RegionId == 0)
            {
                RegionsDAL.InsertRegions(ref eRegions);
            }
            else
            {
                RegionsDAL.UpdateRegions(eRegions);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteRegions(Int32 RegionId)
        {
            //Check if its not in use before Deleting
            RegionsDAL.DeleteRegions(RegionId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
