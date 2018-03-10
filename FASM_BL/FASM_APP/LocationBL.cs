using FASM_DAL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BL.Setups
{
    public class LocationBL
    {
        private LocationDAL LocationDAL = new LocationDAL();

        public DataTable GetLocation()
        {
            return LocationDAL.GetLocation();
        }

        public void LoadLocation(ref Location eLocation)
        {
            LocationDAL.LoadLocation(ref eLocation);
        }

        public FASM_Enums.InfoMessages SaveLocation(ref Location eLocation)
        {
            //Check if already exists
            if (LocationDAL.DoesLocationExists(eLocation.LocationId, eLocation.LocationName) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eLocation.LocationId == 0)
            {
                LocationDAL.InsertLocation(ref eLocation);
            }
            else
            {
                LocationDAL.UpdateLocation(eLocation);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteLocation(Int32 LocationId)
        {
            //Check if its not in use before Deleting
            LocationDAL.DeleteLocation(LocationId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
