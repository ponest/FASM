using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FASM_EN.AssetManagement
{
    public class AssetRegister
    {
        [Required]
        [Display(Name = "Asset Register Id")]
        [Range(Int64.MinValue, Int64.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int64 AssetRegisterId { get; set; }

        [Display(Name = "Serial No")]
        [StringLength(maximumLength: 100)]
        public string SerialNumber { get; set; }

        [Display(Name = "Asset Code")]
        [StringLength(maximumLength: 200)]
        public string AssetCode { get; set; }

        [Display(Name = "Asset Name")]
        public Int64 AssetDefinitionId { get; set; }

        [Display(Name = "Model")]
        [StringLength(maximumLength: 50)]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Condition")]
        [StringLength(maximumLength: 1)]
        public string Condition { get; set; }

        [Required]
        [Display(Name = "Asset Status")]
        [StringLength(maximumLength: 1)]
        public string AssetStatus { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        [Display(Name = "Purchase Date")]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [Display(Name = "Purchase Cost")]
        public decimal PurchaseCost { get; set; }

        [Display(Name = "Warranty Expiry")]
        public DateTime? WarrantyExpiry { get; set; }

        [Display(Name = "Salvage Value")]
        public decimal SalvageValue { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [Required]
        [Display(Name = "Life Time")]
        [Range(Int32.MinValue, Int32.MaxValue, ErrorMessage = "Please enter a valid number")]
        public Int32 LifeTime { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "Custodian")]
        public int CustodianId { get; set; }

        [Display(Name = "Distribution Date")]
        public DateTime? DistributionDate { get; set; }

        [Display(Name = "Reallocated Date")]
        public DateTime? ReallocatedDate { get; set; }

        public bool isLoad { get; set; }

        public string ImagePath { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public System.Data.DataTable dtAssetRegister { get; set; }

    }
}
