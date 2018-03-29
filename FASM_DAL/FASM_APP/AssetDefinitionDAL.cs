using FASM_EN.SQLHelper;
using FASM_EN.AssetManagement;
using System;
using System.Data;

namespace FASM_DAL.AssetManagement
{
    public class AssetDefinitionDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetAssetDefinition()
        {
            query = "SELECT AssetDefinitionId,AssetName,C.CategoryName,BrandName,CASE DepreciationMethod  WHEN 'S' THEN 'Straight Line' ";
            query += " WHEN 'Y' THEN 'Sum of Year Digits' WHEN 'D'  THEN 'Double Declining Method' END ";
            query += " DepreciationMethod FROM AssetDefinition AD INNER JOIN Categories C ON C.CategoryId = AD.CategoryId";



            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadAssetDefinition(ref AssetDefinition eAssetDefinition)
        {
            query = "SELECT AssetDefinitionId,AssetName,CategoryId,BrandName,DepreciationMethod FROM AssetDefinition"; 
            query += " WHERE AssetDefinitionId=@AssetDefinitionId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetDefinitionId", SqlDbType.BigInt, eAssetDefinition.AssetDefinitionId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eAssetDefinition = new AssetDefinition()
                {
                    AssetDefinitionId = Int64.Parse(dr["AssetDefinitionId"].ToString()),
                    AssetName = dr["AssetName"].ToString(),
                    CategoryId = Int32.Parse(dr["CategoryId"].ToString()),
                    BrandName = dr["BrandName"].ToString(),
                    DepreciationMethod = dr["DepreciationMethod"].ToString()
                };
            }
        }
               
        public void InsertAssetDefinition(ref AssetDefinition eAssetDefinition)
        {
            query = "INSERT INTO AssetDefinition(AssetName,CategoryId,BrandName,DepreciationMethod)";
            query+= " VALUES(@AssetName,@CategoryId,@BrandName,@DepreciationMethod); SELECT @AssetDefinitionId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetDefinitionId", SqlDbType.BigInt, eAssetDefinition.AssetDefinitionId, ParameterDirection.Output);
            data.Parameter("@AssetName", SqlDbType.NVarChar, 100, eAssetDefinition.AssetName);
            data.Parameter("@CategoryId", SqlDbType.Int, eAssetDefinition.CategoryId);
            data.Parameter("@BrandName", SqlDbType.NVarChar, 50, eAssetDefinition.BrandName);
            data.Parameter("@DepreciationMethod", SqlDbType.Char, 1, eAssetDefinition.DepreciationMethod);

            data.ExecuteScalar();

            eAssetDefinition.AssetDefinitionId = Int64.Parse(data.GetParamValue("@AssetDefinitionId").ToString());
        }

        public void UpdateAssetDefinition(AssetDefinition eAssetDefinition)
        {
            query = "UPDATE AssetDefinition SET AssetName = @AssetName,CategoryId = @CategoryId,BrandName = @BrandName,";
            query+= "DepreciationMethod = @DepreciationMethod WHERE AssetDefinitionId= @AssetDefinitionId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetDefinitionId", SqlDbType.BigInt, eAssetDefinition.AssetDefinitionId);
            data.Parameter("@AssetName", SqlDbType.NVarChar, 100, eAssetDefinition.AssetName);
            data.Parameter("@CategoryId", SqlDbType.Int, eAssetDefinition.CategoryId);
            data.Parameter("@BrandName", SqlDbType.NVarChar, 50, eAssetDefinition.BrandName);
            data.Parameter("@DepreciationMethod", SqlDbType.Char, 1, eAssetDefinition.DepreciationMethod);

            data.ExecuteScalar();
        }

        public int DoesAssetDefinitionExists(Int64 AssetDefinitionId, string AssetName)
        {
            query = "SELECT COUNT(1) FROM AssetDefinition WHERE AssetDefinitionId <> @AssetDefinitionId AND AssetName = @AssetName";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetDefinitionId", SqlDbType.BigInt, AssetDefinitionId);
            data.Parameter("@AssetName", SqlDbType.NVarChar, 100, AssetName);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteAssetDefinition(Int64 AssetDefinitionId)
        {
            query = "DELETE FROM AssetDefinition WHERE AssetDefinitionId = @AssetDefinitionId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetDefinitionId", SqlDbType.BigInt, AssetDefinitionId);

            data.ExecuteScalar();
        }
    }
}
