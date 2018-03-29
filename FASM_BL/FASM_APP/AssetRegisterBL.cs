using FASM_DAL.AssetManagement;
using FASM_EN.AssetManagement;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BL.AssetManagement
{
    public class AssetRegisterBL
    {
        private AssetRegisterDAL AssetRegisterDAL = new AssetRegisterDAL();

        public DataTable GetAssetRegister()
        {
            return AssetRegisterDAL.GetAssetRegister();
        }

        public void LoadAssetRegister(ref AssetRegister eAssetRegister)
        {
            AssetRegisterDAL.LoadAssetRegister(ref eAssetRegister);
        }

        public FASM_Enums.InfoMessages SaveAssetRegister(ref AssetRegister eAssetRegister)
        {
            //Check if already exists
            if (AssetRegisterDAL.DoesAssetRegisterExists(eAssetRegister.AssetRegisterId, eAssetRegister.AssetCode) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eAssetRegister.AssetRegisterId == 0)
            {
                AssetRegisterDAL.InsertAssetRegister(ref eAssetRegister);
            }
            else
            {
                AssetRegisterDAL.UpdateAssetRegister(eAssetRegister);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteAssetRegister(Int64 AssetRegisterId)
        {
            //Check if its not in use before Deleting
            AssetRegisterDAL.DeleteAssetRegister(AssetRegisterId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
