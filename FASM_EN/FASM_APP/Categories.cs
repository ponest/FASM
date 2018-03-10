using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.Setups
{
    public class Categories
    {
        [Required]
        [Display(Name = "Category Id")]
        public Int32 CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        [StringLength(maximumLength: 50)]
        public string CategoryName { get; set; }

        public System.Data.DataTable dtCategory { get; set; }
    }
}
