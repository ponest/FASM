using FASM_EN.Setups;
using FASM_EN.SQLHelper;
using System;
using System.Data;

namespace FASM_DAL.Setups
{
    public class SuppliersDAL
    {
        private string query = string.Empty;
        private KarazeSQLHelper data = new KarazeSQLHelper();

        public DataTable GetSuppliers()
        {
            query = "SELECT SupplierId,SupplierFullName,CompanyName,RegionId,DistrictId,Website,JobPosition,Phone,Email FROM Suppliers";

            data.SetSqlStatement(query, CommandType.Text);

            return data.FillData();
        }

        public void LoadSuppliers(ref Suppliers eSuppliers)
        {
            query = "SELECT SupplierId,SupplierFullName,CompanyName,RegionId,DistrictId,Website,JobPosition,";
            query += "Phone,Email FROM Suppliers WHERE SupplierId=@SupplierId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@SupplierId", SqlDbType.Int, eSuppliers.SupplierId);

            DataTable dt = data.FillData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                eSuppliers = new Suppliers()
                {
                    SupplierId = Int32.Parse(dr["SupplierId"].ToString()),
                    SupplierFullName = dr["SupplierFullName"].ToString(),
                    CompanyName = dr["CompanyName"].ToString(),
                    RegionId = Int32.Parse(dr["RegionId"].ToString()),
                    DistrictId = Int32.Parse(dr["DistrictId"].ToString()),
                    Website = dr["Website"].ToString(),
                    JobPosition = dr["JobPosition"].ToString(),
                    Phone = dr["Phone"].ToString(),
                    Email = dr["Email"].ToString()
                };
            }
        }

        public void InsertSuppliers(ref Suppliers eSuppliers)
        {
            query = "INSERT INTO Suppliers(SupplierFullName,CompanyName,RegionId,DistrictId,Website,JobPosition,Phone,Email)";
            query += " VALUES(@SupplierFullName,@CompanyName,@RegionId,@DistrictId,@Website,@JobPosition,@Phone,@Email);";
            query += " SELECT @SupplierId = SCOPE_IDENTITY()";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@SupplierId", SqlDbType.Int, eSuppliers.SupplierId, ParameterDirection.Output);
            data.Parameter("@SupplierFullName", SqlDbType.NVarChar, 100, eSuppliers.SupplierFullName);
            data.Parameter("@CompanyName", SqlDbType.NVarChar, 100, eSuppliers.CompanyName);
            data.Parameter("@RegionId", SqlDbType.Int, eSuppliers.RegionId);
            data.Parameter("@DistrictId", SqlDbType.Int, eSuppliers.DistrictId);
            data.Parameter("@Website", SqlDbType.NVarChar, 100, eSuppliers.Website);
            data.Parameter("@JobPosition", SqlDbType.NVarChar, 30, eSuppliers.JobPosition);
            data.Parameter("@Phone", SqlDbType.NVarChar, 20, eSuppliers.Phone);
            data.Parameter("@Email", SqlDbType.NVarChar, 30, eSuppliers.Email);

            data.ExecuteScalar();

            eSuppliers.SupplierId = Int32.Parse(data.GetParamValue("@SupplierId").ToString());
        }

        public void UpdateSuppliers(Suppliers eSuppliers)
        {
            query = "UPDATE Suppliers SET SupplierFullName = @SupplierFullName,CompanyName = @CompanyName,RegionId = @RegionId,";
            query += "DistrictId = @DistrictId,Website = @Website,JobPosition = @JobPosition,Phone = @Phone,Email = @Email";
            query += " WHERE SupplierId= @SupplierId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@SupplierId", SqlDbType.Int, eSuppliers.SupplierId);
            data.Parameter("@SupplierFullName", SqlDbType.NVarChar, 100, eSuppliers.SupplierFullName);
            data.Parameter("@CompanyName", SqlDbType.NVarChar, 100, eSuppliers.CompanyName);
            data.Parameter("@RegionId", SqlDbType.Int, eSuppliers.RegionId);
            data.Parameter("@DistrictId", SqlDbType.Int, eSuppliers.DistrictId);
            data.Parameter("@Website", SqlDbType.NVarChar, 100, eSuppliers.Website);
            data.Parameter("@JobPosition", SqlDbType.NVarChar, 30, eSuppliers.JobPosition);
            data.Parameter("@Phone", SqlDbType.NVarChar, 20, eSuppliers.Phone);
            data.Parameter("@Email", SqlDbType.NVarChar, 30, eSuppliers.Email);

            data.ExecuteScalar();
        }

        public int DoesSuppliersExists(Int32 SupplierId, string Email)
        {
            query = "SELECT COUNT(1) FROM Suppliers WHERE SupplierId <> @SupplierId AND Email = @Email";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@SupplierId", SqlDbType.Int, SupplierId);
            data.Parameter("@Email", SqlDbType.NVarChar, 50, Email);

            return (int)data.FillData().Rows[0][0];
        }

        public void DeleteSuppliers(Int64 SupplierId)
        {
            query = "DELETE FROM Suppliers WHERE SupplierId = @SupplierId";

            data.SetSqlStatement(query, CommandType.Text);

            data.Parameter("@SupplierId", SqlDbType.Int, SupplierId);

            data.ExecuteScalar();
        }
    }
}
