namespace HI.ENTITY
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TAX_PO_HIT_OB_1Purchase_order_headers
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

        [StringLength(30)]
        public string OrderAccount { get; set; }

        [StringLength(30)]
        public string InvoiceAccount { get; set; }

        [StringLength(100)]
        public string PurchName { get; set; }

        [StringLength(10)]
        public string AccountingDate { get; set; }

        [StringLength(30)]
        public string CurrencyCode { get; set; }

        [Required]
        [StringLength(2)]
        public string ChangeRequestRequired { get; set; }

        [StringLength(10)]
        public string DeliveryDate { get; set; }

        [StringLength(4)]
        public string InventSiteId { get; set; }

        [StringLength(5)]
        public string InventLocationId { get; set; }

        [StringLength(30)]
        public string RMtype { get; set; }

        [StringLength(30)]
        public string CompanyChain { get; set; }

        [Column("Head office")]
        [Required]
        [StringLength(1)]
        public string Head_office { get; set; }

        [Column("Total discount", TypeName = "numeric")]
        public decimal? Total_discount { get; set; }

        [Column("Total discount(%)", TypeName = "numeric")]
        public decimal? Total_discount___ { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? Surcharge { get; set; }

        [Required]
        [StringLength(1)]
        public string PostingProfile { get; set; }

        [Required]
        [StringLength(1)]
        public string VendGroup { get; set; }

        [Required]
        [StringLength(1)]
        public string VATNum { get; set; }

        [Required]
        [StringLength(1)]
        public string LanguageId { get; set; }

        [Required]
        [StringLength(1)]
        public string ContactPersonId { get; set; }

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

        [Required]
        [StringLength(1)]
        public string DiscPercent { get; set; }

        [Required]
        [StringLength(1)]
        public string PaymMode { get; set; }

        [Required]
        [StringLength(1)]
        public string TaxGroup { get; set; }

        [Required]
        [StringLength(1)]
        public string PURCHASEORDERSTATUS { get; set; }

        public int FNHSysDeliveryId { get; set; }

        [StringLength(300)]
        public string PurchaseBy { get; set; }

        [StringLength(50)]
        public string WSUser { get; set; }

        [StringLength(300)]
        public string SuperVisorName { get; set; }

        [StringLength(50)]
        public string WSUserSuperVisor { get; set; }

        [StringLength(300)]
        public string TDirectorName { get; set; }

        [StringLength(50)]
        public string WSUserFTDirector { get; set; }

        [StringLength(50)]
        public string VendorAXAccount { get; set; }

        [StringLength(1000)]
        public string REMARK { get; set; }
    }
}
