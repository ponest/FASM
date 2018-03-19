
using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.Setups
{
    public class Location
    {
        [Required]
        [Display(Name = "Location Id")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 LocationId { get; set; }

        [Required]
        [Display(Name = "Location Name")]
        [StringLength(maximumLength: 100)]
        public string LocationName { get; set; }

        public bool isLoad { get; set; }

        public System.Data.DataTable dtLocation { get; set; }

    }
}

