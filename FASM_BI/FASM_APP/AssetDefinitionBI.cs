using FASM_GN;
using FASM_EN.AssetManagement;
using System;
using System.Data;

namespace FASM_BI.AssetManagement
{
    public class AssetDefinitionBI
    {
        public static DataTable GetAssetDefinition()
        {
            AssetDefinitionBL AssetDefinitionBL = new AssetDefinitionBL();
            return AssetDefinitionBL.GetAssetDefinition();
        }

        public static void LoadAssetDefinition(ref AssetDefinition eAssetDefinition)
        {
            AssetDefinitionBL AssetDefinitionBL = new AssetDefinitionBL();
            AssetDefinitionBL.LoadAssetDefinition(ref eAssetDefinition);
        }

        public static FASM_Enums.InfoMessages SaveAssetDefinition(ref AssetDefinition eAssetDefinition)
        {
            AssetDefinitionBL AssetDefinitionBL = new AssetDefinitionBL();
            return AssetDefinitionBL.SaveAssetDefinition(ref eAssetDefinition);
        }

        public static FASM_Enums.InfoMessages DeleteAssetDefinition(Int64 AssetDefinitionId)
        {
            AssetDefinitionBL AssetDefinitionBL = new AssetDefinitionBL();
            return AssetDefinitionBL.DeleteAssetDefinition(AssetDefinitionId);
        }
    }
}
