using FASM_EN.AssetManagement;
using FASM_EN.SQLHelper;
using System;
using System.Data;

namespace FASM_DAL.AssetManagement
{
    public class AssetRegisterDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetAssetRegister()
        {
            query = "SELECT AssetRegisterId,SerialNumber,CONCAT(E.FirstName,' ',E.LastName) Custodian,AssetCode,AD.AssetName,Model,";
            query += " Condition,AssetStatus,SupplierId,PurchaseDate,PurchaseCost,WarrantyExpiry,SalvageValue,L.LocationName,LifeTime,D.DepartmentName,";
            query += " DistributionDate,ReallocatedDate FROM AssetRegister AR";
            query += " INNER JOIN AssetDefinition AD ON AD.AssetDefinitionId = AR.AssetDefinitionId";
            query += " LEFT JOIN Employees E ON E.EmployeeId = AR.CustodianId";
            query += " INNER JOIN Location L ON L.LocationId = AR.LocationId";
            query += " LEFT JOIN Departments D ON D.DepartmentId = AR.DepartmentId";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadAssetRegister(ref AssetRegister eAssetRegister)
        {
            query = "SELECT AssetRegisterId,SerialNumber,AssetCode,AssetDefinitionId,Model,Condition,AssetStatus,SupplierId,PurchaseDate,ImagePath,";
            query += " PurchaseCost,WarrantyExpiry,SalvageValue,LocationId,LifeTime,DepartmentId,CustodianId,DistributionDate,ReallocatedDate";
            query+= " FROM AssetRegister WHERE AssetRegisterId=@AssetRegisterId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetRegisterId", SqlDbType.BigInt, eAssetRegister.AssetRegisterId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eAssetRegister = new AssetRegister();

                eAssetRegister.AssetRegisterId = Int64.Parse(dr["AssetRegisterId"].ToString());
                eAssetRegister.SerialNumber = dr["SerialNumber"].ToString();
                eAssetRegister.AssetCode = dr["AssetCode"].ToString();
                eAssetRegister.AssetDefinitionId = Int64.Parse(dr["AssetDefinitionId"].ToString());
                eAssetRegister.Model = dr["Model"].ToString();
                eAssetRegister.Condition = dr["Condition"].ToString();
                eAssetRegister.AssetStatus = dr["AssetStatus"].ToString();
                eAssetRegister.SupplierId = Int32.Parse(dr["SupplierId"].ToString());
                eAssetRegister.PurchaseDate = DateTime.Parse(dr["PurchaseDate"].ToString());
                eAssetRegister.PurchaseCost = (decimal)dr["PurchaseCost"];
                if (!(dr["WarrantyExpiry"] is DBNull))
                {
                    eAssetRegister.WarrantyExpiry = DateTime.Parse(dr["WarrantyExpiry"].ToString());
                }
                eAssetRegister.SalvageValue = (decimal)dr["SalvageValue"];
                eAssetRegister.LocationId = Int32.Parse(dr["LocationId"].ToString());
                eAssetRegister.LifeTime = Int32.Parse(dr["LifeTime"].ToString());
                eAssetRegister.DepartmentId = Int32.Parse(dr["DepartmentId"].ToString());
                if (!(dr["CustodianId"] is DBNull))
                {
                    eAssetRegister.CustodianId = Int32.Parse(dr["CustodianId"].ToString());
                }
                
                if (!(dr["DistributionDate"] is DBNull))
                {
                    eAssetRegister.DistributionDate = DateTime.Parse(dr["DistributionDate"].ToString());
                }
                if (!(dr["ReallocatedDate"] is DBNull))
                {
                    eAssetRegister.ReallocatedDate = DateTime.Parse(dr["ReallocatedDate"].ToString());
                }
                eAssetRegister.ImagePath = dr["ImagePath"].ToString();
            }
        }

        public void InsertAssetRegister(ref AssetRegister eAssetRegister)
        {
            query = "INSERT INTO AssetRegister(SerialNumber,AssetCode,ImagePath,AssetDefinitionId,Model,Condition,AssetStatus,SupplierId,PurchaseDate,";
            query += "PurchaseCost,WarrantyExpiry,SalvageValue,LocationId,LifeTime,DepartmentId,CustodianId,DistributionDate,ReallocatedDate)";
            query += " VALUES(@SerialNumber,@AssetCode,@ImagePath,@AssetDefinitionId,@Model,@Condition,@AssetStatus,@SupplierId,@PurchaseDate,";
            query += "@PurchaseCost,@WarrantyExpiry,@SalvageValue,@LocationId,@LifeTime,@DepartmentId,@CustodianId,@DistributionDate,";
            query+= " @ReallocatedDate); SELECT @AssetRegisterId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetRegisterId", SqlDbType.BigInt, eAssetRegister.AssetRegisterId, ParameterDirection.Output);
            data.Parameter("@SerialNumber", SqlDbType.NVarChar, 100, eAssetRegister.SerialNumber);
            data.Parameter("@AssetCode", SqlDbType.NVarChar, 200, eAssetRegister.AssetCode);
            data.Parameter("@AssetDefinitionId", SqlDbType.BigInt, eAssetRegister.AssetDefinitionId);
            data.Parameter("@Model", SqlDbType.NVarChar, 50, eAssetRegister.Model);
            data.Parameter("@Condition", SqlDbType.Char, 1, eAssetRegister.Condition);
            data.Parameter("@AssetStatus", SqlDbType.Char, 1, eAssetRegister.AssetStatus);
            data.Parameter("@SupplierId", SqlDbType.Int, eAssetRegister.SupplierId);
            data.Parameter("@PurchaseDate", SqlDbType.DateTime, eAssetRegister.PurchaseDate);
            data.Parameter("@PurchaseCost", SqlDbType.Decimal, eAssetRegister.PurchaseCost);
            data.Parameter("@WarrantyExpiry", SqlDbType.DateTime, eAssetRegister.WarrantyExpiry);
            data.Parameter("@SalvageValue", SqlDbType.Decimal, eAssetRegister.SalvageValue);
            data.Parameter("@LocationId", SqlDbType.Int, eAssetRegister.LocationId);
            data.Parameter("@ImagePath", SqlDbType.NVarChar,250, eAssetRegister.ImagePath);
            data.Parameter("@LifeTime", SqlDbType.Int, eAssetRegister.LifeTime);
            data.Parameter("@DepartmentId", SqlDbType.Int, eAssetRegister.DepartmentId);
            data.Parameter("@CustodianId", SqlDbType.Int, eAssetRegister.CustodianId);
            data.Parameter("@DistributionDate", SqlDbType.DateTime, eAssetRegister.DistributionDate);
            data.Parameter("@ReallocatedDate", SqlDbType.DateTime, eAssetRegister.ReallocatedDate);

            data.ExecuteScalar();

            eAssetRegister.AssetRegisterId = Int64.Parse(data.GetParamValue("@AssetRegisterId").ToString());
        }

        public void UpdateAssetRegister(AssetRegister eAssetRegister)
        {
            query = "UPDATE AssetRegister SET SerialNumber = @SerialNumber,AssetCode = @AssetCode,AssetDefinitionId = @AssetDefinitionId,";
            query += " Model = @Model,Condition = @Condition,AssetStatus = @AssetStatus,SupplierId = @SupplierId,PurchaseDate = @PurchaseDate,";
            query += " PurchaseCost = @PurchaseCost,WarrantyExpiry = @WarrantyExpiry,SalvageValue = @SalvageValue,LocationId = @LocationId,";
            query += " LifeTime = @LifeTime,DepartmentId = @DepartmentId,CustodianId = @CustodianId,DistributionDate = @DistributionDate,";
            query += " ReallocatedDate = @ReallocatedDate,ImagePath = @ImagePath WHERE AssetRegisterId= @AssetRegisterId";


            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetRegisterId", SqlDbType.BigInt, eAssetRegister.AssetRegisterId);
            data.Parameter("@SerialNumber", SqlDbType.NVarChar, 100, eAssetRegister.SerialNumber);
            data.Parameter("@AssetCode", SqlDbType.NVarChar, 200, eAssetRegister.AssetCode);
            data.Parameter("@AssetDefinitionId", SqlDbType.BigInt, eAssetRegister.AssetDefinitionId);
            data.Parameter("@Model", SqlDbType.NVarChar, 50, eAssetRegister.Model);
            data.Parameter("@Condition", SqlDbType.Char, 1, eAssetRegister.Condition);
            data.Parameter("@AssetStatus", SqlDbType.Char, 1, eAssetRegister.AssetStatus);
            data.Parameter("@SupplierId", SqlDbType.Int, eAssetRegister.SupplierId);
            data.Parameter("@PurchaseDate", SqlDbType.DateTime, eAssetRegister.PurchaseDate);
            data.Parameter("@PurchaseCost", SqlDbType.Decimal, eAssetRegister.PurchaseCost);
            data.Parameter("@WarrantyExpiry", SqlDbType.DateTime, eAssetRegister.WarrantyExpiry);
            data.Parameter("@SalvageValue", SqlDbType.Decimal, eAssetRegister.SalvageValue);
            data.Parameter("@LocationId", SqlDbType.Int, eAssetRegister.LocationId);
            data.Parameter("@ImagePath", SqlDbType.NVarChar, 250, eAssetRegister.ImagePath);
            data.Parameter("@LifeTime", SqlDbType.Int, eAssetRegister.LifeTime);
            data.Parameter("@DepartmentId", SqlDbType.Int, eAssetRegister.DepartmentId);
            data.Parameter("@CustodianId", SqlDbType.Int, eAssetRegister.CustodianId);
            data.Parameter("@DistributionDate", SqlDbType.DateTime, eAssetRegister.DistributionDate);
            data.Parameter("@ReallocatedDate", SqlDbType.DateTime, eAssetRegister.ReallocatedDate);

            data.ExecuteScalar();
        }

        public int DoesAssetRegisterExists(Int64 AssetRegisterId, string AssetCode)
        {
            query = "SELECT COUNT(1) FROM AssetRegister WHERE AssetRegisterId <> @AssetRegisterId AND AssetCode = @AssetCode";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetRegisterId", SqlDbType.BigInt, AssetRegisterId);
            data.Parameter("@AssetCode", SqlDbType.NChar,200, AssetCode);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteAssetRegister(Int64 AssetRegisterId)
        {
            query = "DELETE FROM AssetRegister WHERE AssetRegisterId = @AssetRegisterId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@AssetRegisterId", SqlDbType.BigInt, AssetRegisterId);

            data.ExecuteScalar();
        }
    }
}
