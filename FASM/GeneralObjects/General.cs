using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace FASM.GeneralObjects
{
    public class ControllerName
    {
        public static string Setups = "Setups";
        public static string Home = "Home";
        public static string Accounts = "Accounts";
        public static string AssetManagement = "AssetManagement";
    }

    public class General
    {
        public static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }

        public static List<SelectListItem> DataTableToSelectList(DataTable dt, string ValueColomn, string TextColumn, string SelectedValue = null, SelectListItem TopEmptyItem = null)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            if (TopEmptyItem != null)
            {
                list.Add(new SelectListItem()
                {
                    Value = TopEmptyItem.Value,
                    Text = TopEmptyItem.Text,
                    Selected = TopEmptyItem.Selected
                });
            }

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Value = row[ValueColomn].ToString(),
                    Text = row[TextColumn].ToString(),
                    Selected = SelectedValue == null ? false : (row[ValueColomn].ToString() == SelectedValue ? true : false)
                });
            }
            return list;
        }

        public static DataTable GetDepreciationMethods()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");

            dt.Rows.Add("S", "Straight Line");
            dt.Rows.Add("D", "Doubling declining balance");
            dt.Rows.Add("Y", "Sum of years digits ");
            return dt;
        }
    }
    
}