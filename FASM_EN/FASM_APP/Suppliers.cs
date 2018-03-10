using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.Setups
{
    public class Suppliers
    {
        [Required]
        [Display(Name = "Supplier Id")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 SupplierId { get; set; }

        [Required]
        [Display(Name = "Supplier Full Name")]
        [StringLength(maximumLength: 100)]
        public string SupplierFullName { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(maximumLength: 100)]
        public string CompanyName { get; set; }

        public int RegionId { get; set; }

        public int DistrictId { get; set; }

        [Display(Name = "Website")]
        [StringLength(maximumLength: 100)]
        public string Website { get; set; }

        [Display(Name = "Job Position")]
        [StringLength(maximumLength: 30)]
        public string JobPosition { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [StringLength(maximumLength: 20)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(maximumLength: 30)]
        public string Email { get; set; }

        public System.Data.DataTable dtSuppliers { get; set; }

    }
}
