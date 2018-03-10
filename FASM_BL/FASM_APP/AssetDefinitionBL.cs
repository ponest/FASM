using FASM_DAL.AssetManagement;
using FASM_EN.AssetManagement;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BI.AssetManagement
{
    public class AssetDefinitionBL
    {
        private AssetDefinitionDAL AssetDefinitionDAL = new AssetDefinitionDAL();

        public DataTable GetAssetDefinition()
        {
            return AssetDefinitionDAL.GetAssetDefinition();
        }

        public void LoadAssetDefinition(ref AssetDefinition eAssetDefinition)
        {
            AssetDefinitionDAL.LoadAssetDefinition(ref eAssetDefinition);
        }

        public FASM_Enums.InfoMessages SaveAssetDefinition(ref AssetDefinition eAssetDefinition)
        {
            //Check if already exists
            if (AssetDefinitionDAL.DoesAssetDefinitionExists(eAssetDefinition.AssetDefinitionId, eAssetDefinition.AssetName) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eAssetDefinition.AssetDefinitionId == 0)
            {
                AssetDefinitionDAL.InsertAssetDefinition(ref eAssetDefinition);
            }
            else
            {
                AssetDefinitionDAL.UpdateAssetDefinition(eAssetDefinition);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteAssetDefinition(Int64 AssetDefinitionId)
        {
            //Check if its not in use before Deleting
            AssetDefinitionDAL.DeleteAssetDefinition(AssetDefinitionId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
