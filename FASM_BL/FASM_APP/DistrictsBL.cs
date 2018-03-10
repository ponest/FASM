using FASM_DAL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BL.Setups
{
    public class DistrictsBL
    {
        private DistrictsDAL DistrictsDAL = new DistrictsDAL();

        public DataTable GetDistricts()
        {
            return DistrictsDAL.GetDistricts();
        }

        public void LoadDistricts(ref Districts eDistricts)
        {
            DistrictsDAL.LoadDistricts(ref eDistricts);
        }

        public FASM_Enums.InfoMessages SaveDistricts(ref Districts eDistricts)
        {
            //Check if already exists
            if (DistrictsDAL.DoesDistrictsExists(eDistricts.DistrictId, eDistricts.DistrictName) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eDistricts.DistrictId == 0)
            {
                DistrictsDAL.InsertDistricts(ref eDistricts);
            }
            else
            {
                DistrictsDAL.UpdateDistricts(eDistricts);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteDistricts(Int32 DistrictId)
        {
            //Check if its not in use before Deleting
            DistrictsDAL.DeleteDistricts(DistrictId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
