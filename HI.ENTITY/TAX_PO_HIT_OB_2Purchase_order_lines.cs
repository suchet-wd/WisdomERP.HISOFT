namespace HI.ENTITY
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAX_PO_HIT_OB_2Purchase_order_lines
    {
        [Key]
        [Column(Order = 0)]
        public DateTime FDCreateDatetime { get; set; }

        [Key]
        [Column(Order = 1)]
        public long FNRow { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FNState { get; set; }

        [Required]
        [StringLength(1)]
        public string F1 { get; set; }

        [StringLength(30)]
        public string Company { get; set; }

        [Required]
        [StringLength(30)]
        public string PurchId { get; set; }

        public long? LineNumber { get; set; }

        [Required]
        [StringLength(1)]
        public string IVZ_isPromotional_CT { get; set; }

        [StringLength(60)]
        public string ItemId { get; set; }

        [StringLength(2000)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string InventStyleId { get; set; }

        [Required]
        [StringLength(60)]
        public string InventColorId { get; set; }

        [Column("Color name")]
        [StringLength(200)]
        public string Color_name { get; set; }

        [Required]
        [StringLength(60)]
        public string InventSizeId { get; set; }

        [StringLength(60)]
        public string InventSizeId_Fabric { get; set; }

        [Required]
        [StringLength(30)]
        public string inventSerialId { get; set; }

        [Required]
        [StringLength(9)]
        public string IVZ_InventStatusId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PurchQty { get; set; }

        [StringLength(30)]
        public string PurchUnit { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? PurchPrice { get; set; }

        [Required]
        [StringLength(1)]
        public string DiscAmount { get; set; }

        [Required]
        [StringLength(1)]
        public string DiscPercent { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? LineAmount { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Surcharge { get; set; }

        [StringLength(4)]
        public string InventSiteId { get; set; }

        [StringLength(5)]
        public string InventLocationId { get; set; }

        [Required]
        [StringLength(1)]
        public string Dimension1 { get; set; }

        [Required]
        [StringLength(1)]
        public string Dimension2 { get; set; }

        [Required]
        [StringLength(1)]
        public string Dimension3 { get; set; }

        [Required]
        [StringLength(1)]
        public string Dimension4 { get; set; }

        [Required]
        [StringLength(1)]
        public string Dimension5 { get; set; }

        [Required]
        [StringLength(1)]
        public string Dimension6 { get; set; }

        [Required]
        [StringLength(1)]
        public string DeliveryCountryRegionId { get; set; }

        [Required]
        [StringLength(1)]
        public string DeliveryName { get; set; }

        [Required]
        [StringLength(1)]
        public string DeliveryStreet { get; set; }

        [Required]
        [StringLength(1)]
        public string DeliveryCity { get; set; }

        [Required]
        [StringLength(1)]
        public string DeliveryZipCode { get; set; }

        [Required]
        [StringLength(1)]
        public string DeliveryAddress { get; set; }

        [StringLength(10)]
        public string DeliveryDate { get; set; }

        [Column("VAT amount", TypeName = "numeric")]
        public decimal? VAT_amount { get; set; }

        [Required]
        [StringLength(1)]
        public string TaxGroup { get; set; }

        [Required]
        [StringLength(1)]
        public string TaxItemGroup { get; set; }
    }
}
