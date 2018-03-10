using System;
using System.ComponentModel.DataAnnotations;

namespace FASM_EN.AssetManagement
{
    public class AssetDefinition
    {
        [Required]
        [Display(Name = "Asset Definition Id")]
        [Range(Int64.MinValue, Int64.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int64 AssetDefinitionId { get; set; }

        [Required]
        [Display(Name = "Asset Name")]
        [StringLength(maximumLength: 100)]
        public string AssetName { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Brand Name")]
        [StringLength(maximumLength: 50)]
        public string BrandName { get; set; }

        [Required]
        [Display(Name = "Depreciation Method")]
        [StringLength(maximumLength: 1)]
        public string DepreciationMethod { get; set; }

        public System.Data.DataTable dtAssetDefinition { get; set; }

    }
}
