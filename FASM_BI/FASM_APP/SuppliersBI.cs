using FASM_BL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BI.Setups
{
    public class SuppliersBI
    {
        public static DataTable GetSuppliers()
        {
            SuppliersBL SuppliersBL = new SuppliersBL();
            return SuppliersBL.GetSuppliers();
        }

        public static void LoadSuppliers(ref Suppliers eSuppliers)
        {
            SuppliersBL SuppliersBL = new SuppliersBL();
            SuppliersBL.LoadSuppliers(ref eSuppliers);
        }

        public static FASM_Enums.InfoMessages SaveSuppliers(ref Suppliers eSuppliers)
        {
            SuppliersBL SuppliersBL = new SuppliersBL();
            return SuppliersBL.SaveSuppliers(ref eSuppliers);
        }

        public static FASM_Enums.InfoMessages DeleteSuppliers(Int64 SupplierId)
        {
            SuppliersBL SuppliersBL = new SuppliersBL();
            return SuppliersBL.DeleteSuppliers(SupplierId);
        }
    }
}
