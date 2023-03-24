using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HI.ENTITY
{
    public partial class DataInterfaceAX : DbContext
    {
        public DataInterfaceAX()
            : base("data source=wsm-mer;initial catalog=HITECH_SYSLOG;user id=sa;password=[vdw,jwfh;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<TAX_PO_HIT_OB_1Purchase_order_headers> TAX_PO_HIT_OB_1Purchase_order_headers { get; set; }
        public virtual DbSet<TAX_PO_HIT_OB_2Purchase_order_lines> TAX_PO_HIT_OB_2Purchase_order_lines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.F1)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.AccountingDate)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.ChangeRequestRequired)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.DeliveryDate)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.InventSiteId)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.InventLocationId)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Head_office)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Total_discount)
                .HasPrecision(18, 5);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Total_discount___)
                .HasPrecision(18, 5);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Surcharge)
                .HasPrecision(18, 5);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.PostingProfile)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.VendGroup)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.VATNum)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.LanguageId)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.ContactPersonId)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Dimension1)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Dimension2)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Dimension3)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Dimension4)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Dimension5)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.Dimension6)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.DeliveryCountryRegionId)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.DeliveryName)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.DeliveryStreet)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.DeliveryCity)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.DeliveryZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.DeliveryAddress)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.DiscPercent)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.PaymMode)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.TaxGroup)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_1Purchase_order_headers>()
                .Property(e => e.PURCHASEORDERSTATUS)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.F1)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.IVZ_isPromotional_CT)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.IVZ_InventStatusId)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.PurchQty)
                .HasPrecision(19, 5);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.PurchPrice)
                .HasPrecision(18, 5);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.DiscAmount)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.DiscPercent)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.LineAmount)
                .HasPrecision(18, 4);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.Surcharge)
                .HasPrecision(18, 5);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.InventSiteId)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.InventLocationId)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.Dimension1)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.Dimension2)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.Dimension3)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.Dimension4)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.Dimension5)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.Dimension6)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.DeliveryCountryRegionId)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.DeliveryName)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.DeliveryStreet)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.DeliveryCity)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.DeliveryZipCode)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.DeliveryAddress)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.DeliveryDate)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.TaxGroup)
                .IsUnicode(false);

            modelBuilder.Entity<TAX_PO_HIT_OB_2Purchase_order_lines>()
                .Property(e => e.TaxItemGroup)
                .IsUnicode(false);
        }
    }
}
