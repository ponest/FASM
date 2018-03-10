using System.Data;
using FASM_GN;
using FASM_BL.Setups;
using FASM_EN.Setups;
using System;

namespace FASM_BI.Setups
{
    public class DistrictsBI
    {

        public static DataTable GetDistricts()
        {
            DistrictsBL DistrictsBL = new DistrictsBL();
            return DistrictsBL.GetDistricts();
        }

        public static void LoadDistricts(ref Districts eDistricts)
        {
            DistrictsBL DistrictsBL = new DistrictsBL();
            DistrictsBL.LoadDistricts(ref eDistricts);
        }

        public static FASM_Enums.InfoMessages SaveDistricts(ref Districts eDistricts)
        {
            DistrictsBL DistrictsBL = new DistrictsBL();
            return DistrictsBL.SaveDistricts(ref eDistricts);
        }

        public static FASM_Enums.InfoMessages DeleteDistricts(Int32 DistrictId)
        {
            DistrictsBL DistrictsBL = new DistrictsBL();
            return DistrictsBL.DeleteDistricts(DistrictId);
        }
    }
}
