using FASM_BL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BI.Setups
{
    public class LocationBI
    {

        public static DataTable GetLocation()
        {
            LocationBL LocationBL = new LocationBL();
            return LocationBL.GetLocation();
        }

        public static void LoadLocation(ref Location eLocation)
        {
            LocationBL LocationBL = new LocationBL();
            LocationBL.LoadLocation(ref eLocation);
        }

        public static FASM_Enums.InfoMessages SaveLocation(ref Location eLocation)
        {
            LocationBL LocationBL = new LocationBL();
            return LocationBL.SaveLocation(ref eLocation);
        }

        public static FASM_Enums.InfoMessages DeleteLocation(Int32 LocationId)
        {
            LocationBL LocationBL = new LocationBL();
            return LocationBL.DeleteLocation(LocationId);
        }
    }
}
