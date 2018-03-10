using FASM_DAL.Setups;
using FASM_EN.Setups;
using FASM_GN;
using System;
using System.Data;

namespace FASM_BL.Setups
{
    public class CategoriesBL
    {
        private CategoriesDAL CategoriesDAL = new CategoriesDAL();

        public DataTable GetCategories()
        {
            return CategoriesDAL.GetCategories();
        }

        public void LoadCategories(ref Categories eCategories)
        {
            CategoriesDAL.LoadCategories(ref eCategories);
        }

        public FASM_Enums.InfoMessages SaveCategories(ref Categories eCategories)
        {
            //Check if already exists
            if (CategoriesDAL.DoesCategoriesExists(eCategories.CategoryId, eCategories.CategoryName) > 0)
            {
                return FASM_Enums.InfoMessages.AlreadyExist;
            }
            if (eCategories.CategoryId == 0)
            {
                CategoriesDAL.InsertCategories(ref eCategories);
            }
            else
            {
                CategoriesDAL.UpdateCategories(eCategories);
            }
            return FASM_Enums.InfoMessages.Success;
        }

        public FASM_Enums.InfoMessages DeleteCategories(Int32 CategoryId)
        {
            //Check if its not in use before Deleting
            CategoriesDAL.DeleteCategories(CategoryId);
            return FASM_Enums.InfoMessages.Success;
        }
    }
}
