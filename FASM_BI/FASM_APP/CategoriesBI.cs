using System;
using System.Data;
using FASM_GN;
using FASM_BL.Setups;
using FASM_EN.Setups;

namespace FASM_BI.Setups
{
    public class CategoriesBI
    {

        public static DataTable GetCategories()
        {
            CategoriesBL CategoriesBL = new CategoriesBL();
            return CategoriesBL.GetCategories();
        }

        public static void LoadCategories(ref Categories eCategories)
        {
            CategoriesBL CategoriesBL = new CategoriesBL();
            CategoriesBL.LoadCategories(ref eCategories);
        }

        public static FASM_Enums.InfoMessages SaveCategories(ref Categories eCategories)
        {
            CategoriesBL CategoriesBL = new CategoriesBL();
            return CategoriesBL.SaveCategories(ref eCategories);
        }

        public static FASM_Enums.InfoMessages DeleteCategories(Int32 CategoryId)
        {
            CategoriesBL CategoriesBL = new CategoriesBL();
            return CategoriesBL.DeleteCategories(CategoryId);
        }
    }
}
