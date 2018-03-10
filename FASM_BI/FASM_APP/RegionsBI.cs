using System.Data;
using FASM_BL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;

namespace FASM_BI.Setups
{
    public class RegionsBI
    {
        public static DataTable GetRegions()
        {
            RegionsBL RegionsBL = new RegionsBL();
            return RegionsBL.GetRegions();
        }

        public static void LoadRegions(ref Regions eRegions)
        {
            RegionsBL RegionsBL = new RegionsBL();
            RegionsBL.LoadRegions(ref eRegions);
        }

        public static FASM_Enums.InfoMessages SaveRegions(ref Regions eRegions)
        {
            RegionsBL RegionsBL = new RegionsBL();
            return RegionsBL.SaveRegions(ref eRegions);
        }

        public static FASM_Enums.InfoMessages DeleteRegions(Int32 RegionId)
        {
            RegionsBL RegionsBL = new RegionsBL();
            return RegionsBL.DeleteRegions(RegionId);
        }
    }
}

