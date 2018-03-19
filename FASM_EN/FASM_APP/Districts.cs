
using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.Setups
{
    public class Districts
    {
        [Required]
        [Display(Name = "District Id")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 DistrictId { get; set; }

        [Required]
        [Display(Name = "District Name")]
        [StringLength(maximumLength: 100)]
        public string DistrictName { get; set; }

        public bool isLoad { get; set; }

        public System.Data.DataTable dtDistricts { get; set; }
    }
}
