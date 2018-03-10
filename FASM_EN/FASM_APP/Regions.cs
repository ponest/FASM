using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.Setups
{
    public class Regions
    {
        [Required]
        [Display(Name = "Region Id")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 RegionId { get; set; }

        [Required]
        [Display(Name = "Region Name")]
        [StringLength(maximumLength: 100)]
        public string RegionName { get; set; }

        public System.Data.DataTable dtRegion { get; set; }

    }
}
