using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using System;

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

        public static DataTable GetdtParams()
        {
            DataTable dtParams = new DataTable();
            dtParams.Columns.Add("String1", typeof(string));
            dtParams.Columns.Add("String2", typeof(string));
            dtParams.Columns.Add("Int1", typeof(Int32));
            dtParams.Columns.Add("Dec1", typeof(decimal));
            dtParams.Columns.Add("Dec2", typeof(decimal));
            dtParams.Columns.Add("BigInt1", typeof(Int64));
            dtParams.Columns.Add("DateTime1", typeof(DateTime));
            dtParams.Columns.Add("Int2", typeof(Int32));
            dtParams.Columns.Add("Int3", typeof(Int32));
            dtParams.Columns.Add("Int4", typeof(Int32));
            dtParams.Columns.Add("Int5", typeof(Int32));
            dtParams.Columns.Add("Int6", typeof(Int32));
            dtParams.Columns.Add("String3", typeof(string));
            dtParams.Columns.Add("String4", typeof(string));
            dtParams.Columns.Add("String5", typeof(string));
            dtParams.Columns.Add("DateTime2", typeof(DateTime));
            dtParams.Columns.Add("BigInt2", typeof(Int64));

            return dtParams;
        }
    }
    
}