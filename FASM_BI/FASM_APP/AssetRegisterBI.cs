using System;
using System.Data;
using FASM_BL.AssetManagement;
using FASM_EN.AssetManagement;

namespace FASM_BI.AssetManagement
{
    public class AssetRegisterBI
    {

        public static DataTable GetAssetRegister()
        {
            AssetRegisterBL AssetRegisterBL = new AssetRegisterBL();
            return AssetRegisterBL.GetAssetRegister();
        }

        public static void LoadAssetRegister(ref AssetRegister eAssetRegister)
        {
            AssetRegisterBL AssetRegisterBL = new AssetRegisterBL();
            AssetRegisterBL.LoadAssetRegister(ref eAssetRegister);
        }

        public static FASM_GN.FASM_Enums.InfoMessages SaveAssetRegister(ref AssetRegister eAssetRegister)
        {
            AssetRegisterBL AssetRegisterBL = new AssetRegisterBL();
            return AssetRegisterBL.SaveAssetRegister(ref eAssetRegister);
        }

        public static FASM_GN.FASM_Enums.InfoMessages DeleteAssetRegister(Int64 AssetRegisterId)
        {
            AssetRegisterBL AssetRegisterBL = new AssetRegisterBL();
            return AssetRegisterBL.DeleteAssetRegister(AssetRegisterId);
        }
    }
}
