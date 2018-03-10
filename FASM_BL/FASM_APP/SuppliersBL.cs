using FASM_DAL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BL.Setups
{
    public class SuppliersBL
    {
        private SuppliersDAL SuppliersDAL = new SuppliersDAL();

        public DataTable GetSuppliers()
        {
            return SuppliersDAL.GetSuppliers();
        }

        public void LoadSuppliers(ref Suppliers eSuppliers)
        {
            SuppliersDAL.LoadSuppliers(ref eSuppliers);
        }

        public FASM_Enums.InfoMessages SaveSuppliers(ref Suppliers eSuppliers)
        {
            //Check if already exists
            if (SuppliersDAL.DoesSuppliersExists(eSuppliers.SupplierId, eSuppliers.Email) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eSuppliers.SupplierId == 0)
            {
                SuppliersDAL.InsertSuppliers(ref eSuppliers);
            }
            else
            {
                SuppliersDAL.UpdateSuppliers(eSuppliers);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteSuppliers(Int64 SupplierId)
        {
            //Check if its not in use before Deleting
            SuppliersDAL.DeleteSuppliers(SupplierId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
